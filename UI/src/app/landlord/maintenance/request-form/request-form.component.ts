import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { PropertySelectorComponent } from '../property-selector/property-selector.component';
import { PhotoUploadComponent } from '../photo-upload/photo-upload.component';
import { MaintenanceRequest } from '../types/maintenance.types';
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-request-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormFieldComponent,
    PropertySelectorComponent,
    PhotoUploadComponent
  ],
  template: `
    <form [formGroup]="requestForm" (ngSubmit)="handleSubmit()" class="space-y-6">
      <app-property-selector [formGroup]="requestForm" />

      <app-form-field label="Issue Type" id="issueType">
        <select
          id="issueType"
          formControlName="issueType"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
        >
          <option value="">Select issue type</option>
          <option value="plumbing">Plumbing</option>
          <option value="electrical">Electrical</option>
          <option value="hvac">HVAC</option>
          <option value="appliance">Appliance</option>
          <option value="structural">Structural</option>
          <option value="other">Other</option>
        </select>
      </app-form-field>

      <app-form-field label="Priority" id="priority">
        <select
          id="priority"
          formControlName="priority"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
        >
          <option value="">Select priority</option>
          <option value="low">Low</option>
          <option value="medium">Medium</option>
          <option value="high">High</option>
          <option value="emergency">Emergency</option>
        </select>
      </app-form-field>

      <app-form-field label="Description" id="description">
        <textarea
          id="description"
          formControlName="description"
          rows="4"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          placeholder="Describe the issue in detail..."
        ></textarea>
      </app-form-field>

      <app-photo-upload [formGroup]="requestForm" />

      <div class="flex justify-end gap-4">
        <button
          type="submit"
          [disabled]="!requestForm.valid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Submitting...' : 'Submit Request' }}
        </button>
      </div>
    </form>
  `
})
export class RequestFormComponent {
  @Output() submit = new EventEmitter<MaintenanceRequest>();

  requestForm: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder) {
    this.requestForm = this.fb.group({
      propertyId: ['', Validators.required],
      issueType: ['', Validators.required],
      priority: ['', Validators.required],
      description: ['', [Validators.required, Validators.minLength(20)]],
      photos: [[]]
    });
  }

  handleSubmit(): void {
    if (this.requestForm.valid) {
      this.loading = true;
      this.submit.emit(this.requestForm.value);
    }
  }
}