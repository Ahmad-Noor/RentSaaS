import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../../shared/components/form-field/form-field.component';
import { PropertyService } from '../../../../properties/services/property.service';

@Component({
    selector: 'app-property-selector',
    imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
    template: `
    <app-form-field label="Property" id="property">
      <select
        id="property"
        formControlName="property"
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
  properties: any[] = [];

  constructor(private propertyService: PropertyService) {
    this.propertyService.getProperties().subscribe(properties => {
      this.properties = properties;
    });
  }
}