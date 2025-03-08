import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { LeaseService } from "../../../../service/lease.service";
import { LeaseFormDate } from "../../../../models/lease-form.types";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Lease } from "../../../../models/lease.types";
import { PropertyService } from "../../../../service/property.service";
import { EventEmitter, Input, Output, OnInit, Component } from "@angular/core";

@Component({
  selector: 'app-lease-add-edit',
  imports: [RouterLink, CommonModule, ReactiveFormsModule],
  templateUrl: './lease-add-edit.component.html',
  styleUrl: './lease-add-edit.component.css'
})
export class LeaseAddEditComponent implements OnInit {
  leaseForm: FormGroup; // تعريف النموذج
  @Input() lease?: Lease; // إدخال بيانات العقد (إذا تم التعديل)
  @Output() save = new EventEmitter<LeaseFormDate>(); // إخراج حدث الحفظ

  error = ""; // رسالة خطأ
  loading = false; // حالة التحميل
  properties: any[] = []; // قائمة العقارات

  constructor(
    private _fb: FormBuilder, // إنشاء النموذج
    private router: Router, // التوجيه
    private route: ActivatedRoute, // المسار الحالي
    private _propertyService: PropertyService, // خدمة العقارات
    private _leaseService: LeaseService, // خدمة العقود
  ) {
    // تهيئة النموذج
    this.leaseForm = this._fb.group({
      id: "", // معرف العقد (للتعديل)
      propertyId: new FormControl(null, [Validators.required]), // معرف العقار
      tenantName: new FormControl(null, [Validators.required]), // اسم المستأجر
      rentAmount: new FormControl(0, [Validators.required, Validators.min(1)]), // قيمة الإيجار
      startDate: new FormControl(new Date().toISOString().substring(0, 10), [Validators.required]), // تاريخ البدء
      endDate: new FormControl(new Date().toISOString().substring(0, 10), [Validators.required]), // تاريخ الانتهاء
    });
  }

  ngOnInit(): void {
    this.getAllProperties(); // جلب جميع العقارات

    // قراءة معرف العقد من المسار (إذا كان التعديل)
    this.route.params.subscribe(params => {
      const leaseId = params['id'];
      if (leaseId) {
        this.loadLeaseDetails(leaseId); // تحميل بيانات العقد
      }
    });

    // قراءة بيانات العقد من state (إذا تم تمريرها)
    const leaseData = history.state.lease;
    if (leaseData) {
      this.leaseForm.patchValue(leaseData); // تعبئة النموذج بالبيانات
    }
  }

  // تحميل بيانات العقد
  loadLeaseDetails(id: string): void {
    this._leaseService.getLeaseById(id).subscribe({
      next: (lease) => {
        // تحويل التواريخ إلى تنسيق صحيح
        if (lease.startDate) {
          lease.startDate = new Date(lease.startDate).toISOString().split("T")[0];
        }
        if (lease.endDate) {
          lease.endDate = new Date(lease.endDate).toISOString().split("T")[0];
        }
        this.leaseForm.patchValue(lease); // تعبئة النموذج بالبيانات
      },
      error: (err) => {
        console.error("Error loading lease details:", err);
      },
    });
  }

  // عند إرسال النموذج
  onFormSubmit(): void {
    if (this.leaseForm.valid) {
      this.loading = true; // بدء التحميل
      const leaseData = { ...this.leaseForm.value }; // نسخ بيانات النموذج

      // تحويل التواريخ إلى تنسيق صحيح
      if (leaseData.startDate) {
        const startDate = new Date(leaseData.startDate);
        leaseData.startDate = !isNaN(startDate.getTime()) ? startDate.toISOString().split("T")[0] : null;
      }
      if (leaseData.endDate) {
        const endDate = new Date(leaseData.endDate);
        leaseData.endDate = !isNaN(endDate.getTime()) ? endDate.toISOString().split("T")[0] : null;
      }

      console.log("Lease Data to Submit:", leaseData); // تحقق من البيانات

      if (leaseData.id) {
        // تحديث العقد الموجود
        this._leaseService.updateLease(leaseData.id, leaseData).subscribe({
          next: () => {
            this.loading = false; // إيقاف التحميل
            this.router.navigate(['/landlord/properties/lease']); // إعادة التوجيه إلى صفحة القائمة
          },
          error: (err: any) => {
            this.loading = false; // إيقاف التحميل
            console.error("Error updating lease", err);
            this.error = err.error?.message || "Failed to update lease. Please try again.";
          },
        });
      } else {
        // إضافة عقد جديد
        this._leaseService.addLease(leaseData).subscribe({
          next: () => {
            this.loading = false; // إيقاف التحميل
            this.router.navigate(['/landlord/properties/lease']); // إعادة التوجيه إلى صفحة القائمة
          },
          error: (err: any) => {
            this.loading = false; // إيقاف التحميل
            console.error("Error adding lease", err);
            this.error = err.error?.message || "Failed to add lease. Please try again.";
          },
        });
      }
    } else {
      // إذا كان النموذج غير صالح
      Object.keys(this.leaseForm.controls).forEach((key) => {
        this.leaseForm.get(key)?.markAsTouched(); // تمييز الحقول التي تحتاج إلى إدخال
      });
    }
  }

  // جلب جميع العقارات
  getAllProperties() {
    this._propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        this.properties = properties.data; // تعيين قائمة العقارات
      },
      error: (err) => {
        console.error("Error loading properties:", err);
      },
    });
  }
}