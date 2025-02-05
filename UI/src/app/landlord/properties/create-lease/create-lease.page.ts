import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms'; 
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component';
import { PropertySelectorComponent } from '../property-selector/property-selector.component';
import { LeaseService } from '../services/lease.service';
import { CreateLeaseDTO } from '../types/lease.types';

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
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Create Lease Agreement</h1>
          <p class="mt-1 text-gray-600">Create a new lease agreement for your property</p>
        </div>
        <a 
          routerLink=".."
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Leases
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <form [formGroup]="leaseForm" (ngSubmit)="handleSubmit()" class="space-y-6">
            <app-property-selector [formGroup]="leaseForm" />

            <div class="grid grid-cols-2 gap-4">
              <app-form-field label="Tenant Name" id="tenantName">
                <input
                  type="text"
                  id="tenantName"
                  formControlName="tenantName"
                  class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                  placeholder="Enter tenant's full name"
                >
              </app-form-field>

              <app-form-field label="Lease Type" id="type">
                <select
                  id="type"
                  formControlName="type"
                  class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                >
                  <option value="">Select lease type</option>
                  <option value="standard">Standard</option>
                  <option value="month-to-month">Month-to-Month</option>
                  <option value="sublease">Sublease</option>
                  <option value="renewal">Renewal</option>
                </select>
              </app-form-field>
            </div>

            <div class="grid grid-cols-2 gap-4">
              <app-form-field label="Start Date" id="startDate">
                <input
                  type="date"
                  id="startDate"
                  formControlName="startDate"
                  class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                >
              </app-form-field>

              <app-form-field label="End Date" id="endDate">
                <input
                  type="date"
                  id="endDate"
                  formControlName="endDate"
                  class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                >
              </app-form-field>
            </div>

            <app-form-field label="Monthly Rent" id="monthlyRent">
              <div class="relative">
                <span class="absolute left-3 top-2 text-gray-500">$</span>
                <input
                  type="number"
                  id="monthlyRent"
                  formControlName="monthlyRent"
                  class="w-full pl-8 p-2 border rounded focus:ring-2 focus:ring-blue-500"
                  min="0"
                  step="0.01"
                >
              </div>
            </app-form-field>

            <div class="flex justify-end gap-4">
              <a 
                routerLink=".."
                class="px-4 py-2 text-gray-700 hover:text-gray-900"
              >
                Cancel
              </a>
              <button
                type="submit"
                [disabled]="!leaseForm.valid || loading"
                class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
              >
                {{ loading ? 'Creating...' : 'Create Lease' }}
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  `
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