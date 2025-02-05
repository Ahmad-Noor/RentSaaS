import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-vendor-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <div [formGroup]="formGroup">
      <app-form-field label="Vendor" id="vendor">
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
    </div>
  `
})
export class VendorSelectorComponent {
  @Input() formGroup!: FormGroup;

  // This would typically come from a service
  vendors = [
    { id: '1', name: 'ABC Maintenance' },
    { id: '2', name: 'City Utilities' },
    { id: '3', name: 'XYZ Contractors' },
    { id: '4', name: 'Local Tax Office' }
  ];
}