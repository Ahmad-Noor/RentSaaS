import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-tenant-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <app-form-field label="Tenant" id="tenant">
      <select
        id="tenant"
        formControlName="tenant"
        class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
      >
        <option value="">Select tenant</option>
        @for (tenant of tenants; track tenant.id) {
          <option [value]="tenant.id">{{ tenant.name }}</option>
        }
      </select>
    </app-form-field>
  `
})
export class TenantSelectorComponent {
  @Input() formGroup!: FormGroup;

  // Mock data - would typically come from a service
  tenants = [
    { id: '1', name: 'John Doe' },
    { id: '2', name: 'Jane Smith' },
    { id: '3', name: 'Bob Johnson' }
  ];
}