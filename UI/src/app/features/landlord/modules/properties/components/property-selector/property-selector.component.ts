import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';
import { PropertyService } from '../../services/property.service';

@Component({
  selector: 'app-property-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <div [formGroup]="formGroup">
      <app-form-field label="Property" id="propertyId">
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
    </div>
  `
})
export class PropertySelectorComponent {
  @Input() formGroup!: FormGroup;
  properties: any[] = [];

  constructor(private propertyService: PropertyService) {
    this.propertyService.getAllProperties().subscribe(properties => {
      this.properties = properties;
    });
  }
}