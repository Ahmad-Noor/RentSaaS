import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component';
import { PropertySelectorComponent } from '../property-selector/property-selector.component';
import { ListingPhotosComponent } from './listing-photos.component';
import { ListingPlatformsComponent } from './listing-platforms.component';
import { ListingFormData } from '../../../models/listing.types';

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
  templateUrl:"listing-form.component.html"
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