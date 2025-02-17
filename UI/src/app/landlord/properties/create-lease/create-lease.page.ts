import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; 
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component';
import { PropertySelectorComponent } from '../property-selector/property-selector.component'; 
import { CreateLeaseDTO } from '../../../models/lease.types';
import { LeaseService } from '../../../service/lease.service';

@Component({
  selector: 'app-create-lease-page',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink,
    ReactiveFormsModule,
    FormFieldComponent,
    PropertySelectorComponent
  ],
  templateUrl:"create-lease.page.html"
})
export class CreateLeasePage {
  leaseForm: FormGroup;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private leaseService: LeaseService
  ) {
    this.leaseForm = this.fb.group({
      propertyId: ['', Validators.required],
      tenantName: ['', Validators.required],
      type: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      monthlyRent: ['', [Validators.required, Validators.min(0)]]
    });
  }

  handleSubmit(): void {
    if (this.leaseForm.valid) {
      this.loading = true;
      const leaseData: CreateLeaseDTO = {
        propertyId: Number(this.leaseForm.value.propertyId),
        tenantName: this.leaseForm.value.tenantName,
        type: this.leaseForm.value.type,
        startDate: this.leaseForm.value.startDate,
        endDate: this.leaseForm.value.endDate,
        monthlyRent: Number(this.leaseForm.value.monthlyRent)
      };

      this.leaseService.createLease(leaseData);
      this.router.navigate(['..'], { relativeTo: this.route });
    }
  }
}