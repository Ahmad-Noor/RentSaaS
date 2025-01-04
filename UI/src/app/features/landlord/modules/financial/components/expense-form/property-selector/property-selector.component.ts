import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../../shared/components/form-field/form-field.component';

@Component({
    selector: 'app-property-selector',
    imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
    template: `
    <app-form-field 
      label="Property" 
      id="propertyId"
      [error]="getFieldError('propertyId')"
    >
      <select
        id="propertyId"
        formControlName="propertyId"
        class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
      >
        <option value="">Select property</option>
        @for (property of properties; track property.id) {
          <option [value]="property.id">{{ property.name }}</option>
        }
      </select>
    </app-form-field>
  `
})
export class PropertySelectorComponent {
  @Input() formGroup!: FormGroup;

  // Mock data - in real app would come from a service
  properties = [
    { id: 1, name: 'Sunset Apartments' },
    { id: 2, name: 'Downtown Lofts' },
    { id: 3, name: 'Highland House' }
  ];

  getFieldError(field: string): string {
    const control = this.formGroup.get(field);
    if (control?.touched && control.errors) {
      if (control.errors['required']) {
        return `Property selection is required`;
      }
    }
    return '';
  }
}