import { Component, Input } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormGroup, ReactiveFormsModule } from "@angular/forms";

@Component({
  selector: "app-property-selector",
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, ],
  template: `
    <!-- <div [formGroup]="formGroup">
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
          <option *ngFor="let property of properties" [value]="property.id">
            {{ property.name }}
          </option>
        </select>
      </app-form-field>
    </div> -->
  `,
})
export class PropertySelectorComponent {
  @Input() formGroup!: FormGroup;

  properties = [
    { id: 1, name: "Sunset Apartments" },
    { id: 2, name: "Downtown Lofts" },
    { id: 3, name: "Highland House" },
  ];

  getFieldError(field: string): string {
    const control = this.formGroup.get(field);
    if (control?.touched && control.errors) {
      if (control.errors["required"]) {
        return "Property selection is required";
      }
    }
    return "";
  }
}
