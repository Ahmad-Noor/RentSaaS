import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-vendor-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <app-form-field 
      label="Vendor" 
      id="vendor"
      [error]="getFieldError('vendor')"
    >
      <select
        id="vendor"
        formControlName="vendor"
        class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
      >
        <option value="">Select vendor</option>
        @for (vendor of vendors; track vendor.id) {
          <option [value]="vendor.id">{{ vendor.name }}</option>
        }
      </select>
    </app-form-field>
  `
})
export class VendorSelectorComponent {
  @Input() formGroup!: FormGroup;

  // Mock data - in real app would come from a service
  vendors = [
    { id: 1, name: 'ABC Maintenance' },
    { id: 2, name: 'City Utilities' },
    { id: 3, name: 'XYZ Contractors' }
  ];

  getFieldError(field: string): string {
    const control = this.formGroup.get(field);
    if (control?.touched && control.errors) {
      if (control.errors['required']) {
        return `Vendor selection is required`;
      }
    }
    return '';
  }
}