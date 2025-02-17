import { Component, Input, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component';
import { PropertyService } from '../../../service/property.service';

export interface AllPropertIes {
  address: string;
  id: string;
  // Add other fields as needed
}

@Component({
  selector: 'app-property-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  templateUrl: 'property-selector.component.html',
})
export class PropertySelectorComponent {
  @Input() formGroup!: FormGroup;
  properties: AllPropertIes[] = [];
  isLoading: boolean = true;

  constructor(private propertyService: PropertyService, private cd: ChangeDetectorRef) {
    this.loadProperties();
  }

  loadProperties(): void {
    this.propertyService.getAllProperties().subscribe({
      next: (properties) => {
        console.log('API Response:', properties);

        // Ensure the response is an array and map to AllPropertIes
        this.properties = Array.isArray(properties) ? properties.map((property) => ({
          address: property.address ?? 'No Address', // Default if address is missing
          id: property.id ?? '', // Ensure id is always a string
        })) : [];

        this.isLoading = false;
        this.cd.detectChanges(); // Force view update
      },
      error: (err) => {
        console.error('Failed to fetch properties:', err);
        this.isLoading = false;
      },
    });
  }
}
