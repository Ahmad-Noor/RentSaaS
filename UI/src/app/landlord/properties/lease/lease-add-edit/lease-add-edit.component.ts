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
  leaseForm: FormGroup;
  @Input() lease?: Lease;
  @Output() save = new EventEmitter<LeaseFormDate>();

  error = "";
  loading = false;
  properties: any[] = [];

  constructor(
    private _fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private _propertyService: PropertyService,
    private _leaseService: LeaseService,
  ) {
    this.leaseForm = this._fb.group({
      id: "",
      propertyId: new FormControl(null, [Validators.required]),
      tenantName: new FormControl(null, [Validators.required]),
      rentAmount: new FormControl(0, [Validators.required, Validators.min(1)]),
      startDate: new FormControl(new Date().toISOString().substring(0, 10), [Validators.required]),
      endDate: new FormControl(new Date().toISOString().substring(0, 10), [Validators.required]),
    });
  }

  ngOnInit(): void {
    this.getAllProperties();

    this.route.params.subscribe(params => {
      const leaseId = params['id'];
      if (leaseId) {
        this.loadLeaseDetails(leaseId);
      }
    });

    const leaseData = history.state.lease;
    if (leaseData) {
      this.leaseForm.patchValue(leaseData);
    }
  }

  loadLeaseDetails(id: string): void {
    this._leaseService.getLeaseById(id).subscribe({
      next: (lease) => {
        if (lease.startDate) {
          lease.startDate = new Date(lease.startDate).toISOString().split("T")[0];
        }
        if (lease.endDate) {
          lease.endDate = new Date(lease.endDate).toISOString().split("T")[0];
        }
        this.leaseForm.patchValue(lease);
      },
      error: (err) => {
        console.error("Error loading lease details:", err);
      },
    });
  }

  onFormSubmit(): void {
    if (this.leaseForm.valid) {
      this.loading = true;
      const leaseData = { ...this.leaseForm.value };

      if (leaseData.startDate) {
        const startDate = new Date(leaseData.startDate);
        leaseData.startDate = !isNaN(startDate.getTime()) ? startDate.toISOString().split("T")[0] : null;
      }
      if (leaseData.endDate) {
        const endDate = new Date(leaseData.endDate);
        leaseData.endDate = !isNaN(endDate.getTime()) ? endDate.toISOString().split("T")[0] : null;
      }

      console.log("Lease Data to Submit:", leaseData);

      if (leaseData.id) {
        this._leaseService.updateLease(leaseData.id, leaseData).subscribe({
          next: () => {
            this.loading = false;
            this.router.navigate(['/landlord/properties/lease']);
          },
          error: (err: any) => {
            this.loading = false;
            console.error("Error updating lease", err);
            this.error = err.error?.message || "Failed to update lease. Please try again.";
          },
        });
      } else {
        this._leaseService.addLease(leaseData).subscribe({
          next: () => {
            this.loading = false;
            this.router.navigate(['/landlord/properties/lease']);
          },
          error: (err: any) => {
            this.loading = false;
            console.error("Error adding lease", err);
            this.error = err.error?.message || "Failed to add lease. Please try again.";
          },
        });
      }
    } else {
      Object.keys(this.leaseForm.controls).forEach((key) => {
        this.leaseForm.get(key)?.markAsTouched();
      });
    }
  }

  getAllProperties() {
    this._propertyService.getAllProperties().subscribe({
      next: (properties: any) => {
        this.properties = properties.data;
      },
      error: (err) => {
        console.error("Error loading properties:", err);
      },
    });
  }
}
