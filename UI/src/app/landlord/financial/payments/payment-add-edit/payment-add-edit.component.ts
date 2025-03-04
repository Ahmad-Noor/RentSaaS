import { CommonModule } from "@angular/common";
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { PaymentService } from "../../../../service/payment.service";
import { PaymentFormData } from "../../../../models/payment-form.types";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from "@angular/forms";
import { Payment } from "../../../../models/payment.types";
import { Company } from "../../../../models/company.types";
import { FileItemComponent } from "./file-item.component";
import { CompanyService } from "../../../../service/company.service";
import { PropertyService } from "../../../../service/property.service";
import { TenantService } from "../../../../service/tenant.service";

import { FileWithMetadata } from "../../../../models/fileWithMetadata.types";
import { EventEmitter, Input, Output, OnInit, Component } from "@angular/core";

@Component({
  selector: 'app-payment-add-edit',
  imports: [RouterLink, CommonModule, ReactiveFormsModule, FileItemComponent],
  templateUrl: './payment-add-edit.component.html',
  styleUrl: './payment-add-edit.component.css'
})
export class PaymentAddEditComponent {
paymentForm: FormGroup;
  @Input() payment?: Payment;
  @Output() save = new EventEmitter<PaymentFormData>();

  files: FileWithMetadata[] = [];
  filesToDelete: string[] = [];
  error = "";
  loading = false;
  properties: any[] = [];
  tenanties:any[] = [];
  companies!: Company[];

  constructor(
    private _fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _propertyService: PropertyService,
    private _paymentService: PaymentService,
    private _companyService: CompanyService,
    private _tenantService:TenantService
    
  ) {
    this.paymentForm = this._fb.group({
      id: "",
      propertyId: new FormControl(null),
      tenantId:new FormControl(null),
      paymentType: new FormControl("property"),
      tenant : new FormControl("tenant"),
      amount: new FormControl(0, [Validators.required]),
      dueDate: new FormControl(new Date().toISOString().substring(0, 10), [
        Validators.required,
      ]),
      details: new FormControl(null),
      type: new FormControl("property"),
      files: new FormControl([]),
    });
  }

  ngOnInit(): void {
    this.getCompany();
    this.getAllProperties();

    // Check if we're editing an existing expense
    const paymentData = history.state.payment;
    if (paymentData) {
      this.loadPaymentDetails(paymentData.id);
    }
  }

  loadPaymentDetails(paymentId: string): void {
    this._paymentService.getPaymentById(paymentId).subscribe({
      next: (payment) => {
        if (payment.dueDate) {
          payment.dueDate = new Date(payment.dueDate)
            .toISOString()
            .substring(0, 10);
        }
        this.paymentForm.patchValue(payment);
 
        if (payment.files) {
          this.files = payment.files.map((file: any) => ({
            id: file.id,
            name: file.fileName,
            size: file.fileSize,
            type: file.fileName.endsWith(".pdf")
              ? "application/pdf"
              : "image/*",
            file: new File([], file.fileName),
            url: file.url,
          }));
        }
      },
      error: (err) => {
        console.error("Error loading payment details:", err);
      },
    });
  }
  onFormSubmit(): void {
    if (this.paymentForm.valid) {
      const paymentData = this.paymentForm.value;

      // Format date for API
      if (paymentData.dueDate) {
        const date = new Date(paymentData.dueDate);
        if (!isNaN(date.getTime())) {
          paymentData.dueDate = date.toISOString().split("T")[0];
        }
      }

      // Prepare file files
      const files = this.files.map((file) => file.file);
      paymentData.filesToDelete = this.filesToDelete;

      // Update or create expense
      if (paymentData.id) {
        this._paymentService
          .updatePayment(paymentData.id, paymentData, files)
          .subscribe({
            next: (val: any) => {
              this.router.navigate([".."], { relativeTo: this.route });
            },
            error: (err: any) => {
              console.error("Error updating expense", err);
              // Add error handling here to display to user
            },
          });
      } else {
        // For adding new expense
        this._paymentService.addPayment(paymentData, files).subscribe({
          next: (val: any) => {
            this.router.navigate([".."], { relativeTo: this.route });
          },
          error: (err: any) => {
            console.error("Error adding payment", err);
            if (err.error && err.error.message) {
              this.error = err.error.message;
            } else {
              this.error = "Failed to add payment. Please try again.";
            }
          },
        });
      }
    } else {
      // Mark all form controls as touched to show validation errors
      Object.keys(this.paymentForm.controls).forEach((key) => {
        const control = this.paymentForm.get(key);
        control?.markAsTouched();
      });
    }
  }
  getCompany() {
    this._companyService.getCompanies().subscribe({
      next: (result) => {
        this.companies = result.data;
      },
      error: (result) => {},
    });
  }

  getAllProperties() {
    this._propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        this.properties = properties.data;
      },
      error: (properties) => {},
      complete: () => {},
    });
  }

  onFilesSelected(event: any): void {
    const files = Array.from((event.target as HTMLInputElement).files || []);

    if (this.files.length + files.length > 5) {
      this.error = "You can upload a maximum of 5 files";
      return;
    }

    this.error = ""; // Clear any previous errors

    files.forEach((fileInfo: any) => {
      const validation = validateFileWithMetadata(fileInfo);
      if (!validation.isValid) {
        this.error = validation.error || "Invalid file";
        return;
      }

      const file: FileWithMetadata = {
        id: crypto.randomUUID(),
        file: fileInfo,
        name: fileInfo.name,
        size: fileInfo.size,
        type: fileInfo.type,
      };

      this.files.push(file);
    });

    // Reset the file input
    (event.target as HTMLInputElement).value = "";
  }

  removeFile(file: any): void {
    this.files = this.files.filter((r) => r.id !== file.id);
    this.filesToDelete.push(file.id);
    this.error = "";
  }
}

function validateFileWithMetadata(file: FileWithMetadata): {
  isValid: boolean;
  error?: string;
} {
  const validTypes = ["image/jpeg", "image/png", "application/pdf"];
  const maxSize = 5 * 1024 * 1024; // 5MB

  if (!validTypes.includes(file.type)) {
    return {
      isValid: false,
      error: "Invalid file type. Please upload JPG, PNG or PDF only.",
    };
  }

  if (file.size > maxSize) {
    return {
      isValid: false,
      error: "FileWithMetadata size exceeds 5MB limit.",
    };
  }

  return { isValid: true };
}
