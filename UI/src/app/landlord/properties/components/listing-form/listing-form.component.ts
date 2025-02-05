import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component';
import { PropertySelectorComponent } from '../property-selector/property-selector.component';
import { ListingPhotosComponent } from './listing-photos.component';
import { ListingPlatformsComponent } from './listing-platforms.component';
import { ListingFormData } from '../../types/listing.types';

@Component({
  selector: 'app-listing-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormFieldComponent,
    PropertySelectorComponent,
    ListingPhotosComponent,
    ListingPlatformsComponent
  ],
  template: `
    <form [formGroup]="listingForm" (ngSubmit)="handleSubmit()" class="space-y-6">
      <app-property-selector [formGroup]="listingForm" />

      <div class="grid grid-cols-2 gap-4">
        <app-form-field label="Monthly Rent" id="rent">
          <div class="relative">
            <span class="absolute left-3 top-2 text-gray-500">$</span>
            <input
              type="number"
              id="rent"
              formControlName="rent"
              class="w-full pl-8 p-2 border rounded focus:ring-2 focus:ring-blue-500"
              min="0"
              step="0.01"
            >
          </div>
        </app-form-field>

        <app-form-field label="Security Deposit" id="deposit">
          <div class="relative">
            <span class="absolute left-3 top-2 text-gray-500">$</span>
            <input
              type="number"
              id="deposit"
              formControlName="deposit"
              class="w-full pl-8 p-2 border rounded focus:ring-2 focus:ring-blue-500"
              min="0"
              step="0.01"
            >
          </div>
        </app-form-field>
      </div>

      <app-form-field label="Description" id="description">
        <textarea
          id="description"
          formControlName="description"
          rows="4"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          placeholder="Describe your property..."
        ></textarea>
      </app-form-field>

      <div class="grid grid-cols-2 gap-4">
        <app-form-field label="Available From" id="availableFrom">
          <input
            type="date"
            id="availableFrom"
            formControlName="availableFrom"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          >
        </app-form-field>

        <app-form-field label="Lease Term" id="leaseTerm">
          <select
            id="leaseTerm"
            formControlName="leaseTerm"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          >
            <option value="">Select lease term</option>
            <option value="12">12 months</option>
            <option value="6">6 months</option>
            <option value="month">Month-to-month</option>
          </select>
        </app-form-field>
      </div>

      <app-listing-photos [formGroup]="listingForm" />
      
      <app-listing-platforms [formGroup]="listingForm" />

      <div class="flex justify-end gap-4">
        <button
          type="submit"
          [disabled]="!listingForm.valid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Creating...' : 'Create Listing' }}
        </button>
      </div>
    </form>
  `
})
export class ListingFormComponent {
  @Output() submit = new EventEmitter<ListingFormData>();

  listingForm: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder) {
    this.listingForm = this.fb.group({
      propertyId: ['', Validators.required],
      rent: ['', [Validators.required, Validators.min(0)]],
      deposit: ['', [Validators.required, Validators.min(0)]],
      description: ['', [Validators.required, Validators.minLength(100)]],
      availableFrom: ['', Validators.required],
      leaseTerm: ['', Validators.required],
      photos: [[]],
      platforms: this.fb.group({
        zillow: [false],
        trulia: [false],
        apartments: [false],
        realtor: [false]
      })
    });
  }

  handleSubmit(): void {
    if (this.listingForm.valid) {
      this.loading = true;
      this.submit.emit(this.listingForm.value);
    }
  }
}