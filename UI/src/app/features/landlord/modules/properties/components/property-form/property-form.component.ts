import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-property-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, FormFieldComponent],
  template: `
    <form [formGroup]="propertyForm" (ngSubmit)="handleSubmit()" class="space-y-6">
      <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
        <app-form-field 
          label="Street address" 
          id="address"
          [error]="getFieldError('address')"
        >
          <input
            type="text"
            id="address"
            formControlName="address"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
            placeholder="Enter the USPS-validated address"
          >
          <p class="mt-1 text-sm text-gray-500">
            You won't be able to edit the address once you create the listing
          </p>
        </app-form-field>

        <div class="grid grid-cols-2 gap-4">
          <app-form-field 
            label="Property type" 
            id="propertyType"
            [error]="getFieldError('propertyType')"
          >
            <select
              id="propertyType"
              formControlName="propertyType"
              class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
            >
              <option value="">Please select</option>
              <option value="house">House</option>
              <option value="condo">Condo / Apartment Unit</option>
              <option value="townhouse">Townhouse</option>
              <option value="community">Entire Apartment Community</option>
            </select>
          </app-form-field>

          <app-form-field 
            label="Unit number" 
            id="unitNumber"
            [error]="getFieldError('unitNumber')"
          >
            <input
              type="text"
              id="unitNumber"
              formControlName="unitNumber"
              class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
              placeholder="#"
            >
            <p class="mt-1 text-sm text-gray-500">If applicable</p>
          </app-form-field>
        </div>
      </div>

      <div class="flex justify-end gap-4">
        <a 
          routerLink=".."
          class="px-4 py-2 text-gray-700 bg-white border rounded hover:bg-gray-50"
        >
          Cancel
        </a>
        <button
          type="submit"
          [disabled]="propertyForm.invalid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Creating...' : 'Create Property' }}
        </button>
      </div>
    </form>
  `
})
export class PropertyFormComponent {
  @Output() onSubmit = new EventEmitter<any>();
  propertyForm: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder) {
    this.propertyForm = this.fb.group({
      address: ['', [Validators.required]],
      propertyType: ['', [Validators.required]],
      unitNumber: ['']
    });
  }

  getFieldError(field: string): string {
    const control = this.propertyForm.get(field);
    if (control?.touched && control.errors) {
      if (control.errors['required']) {
        return `${field.charAt(0).toUpperCase() + field.slice(1)} is required`;
      }
    }
    return '';
  }

  handleSubmit(): void {
    if (this.propertyForm.valid) {
      this.loading = true;
      this.onSubmit.emit(this.propertyForm.value);
    }
  }
}