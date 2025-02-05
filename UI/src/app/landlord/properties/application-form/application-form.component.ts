import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../shared/components/form-field/form-field.component'; 
import { PropertySelectorComponent } from '../property-selector/property-selector.component';

@Component({
  selector: 'app-application-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent, PropertySelectorComponent],
  template: `
    <form [formGroup]="applicationForm" (ngSubmit)="handleSubmit()" class="space-y-6">
      <app-property-selector [formGroup]="applicationForm" />

      <div class="grid grid-cols-2 gap-4">
        <app-form-field label="Applicant Email" id="email">
          <input
            type="email"
            id="email"
            formControlName="email"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
            placeholder="tenant@example.com"
          >
        </app-form-field>

        <app-form-field label="Phone Number" id="phone">
          <input
            type="tel"
            id="phone"
            formControlName="phone"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
            placeholder="(555) 123-4567"
          >
        </app-form-field>
      </div>

      <app-form-field label="Message" id="message">
        <textarea
          id="message"
          formControlName="message"
          rows="4"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          placeholder="Add a personal message to the applicant..."
        ></textarea>
      </app-form-field>

      <div class="flex items-center gap-2">
        <input
          type="checkbox"
          id="requestBackground"
          formControlName="requestBackground"
          class="rounded border-gray-300"
        >
        <label for="requestBackground" class="text-sm text-gray-700">
          Request background check
        </label>
      </div>

      <div class="flex items-center gap-2">
        <input
          type="checkbox"
          id="requestCredit"
          formControlName="requestCredit"
          class="rounded border-gray-300"
        >
        <label for="requestCredit" class="text-sm text-gray-700">
          Request credit report
        </label>
      </div>

      <div class="flex justify-end gap-4">
        <button
          type="submit"
          [disabled]="!applicationForm.valid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Sending...' : 'Send Application' }}
        </button>
      </div>
    </form>
  `
})
export class ApplicationFormComponent {
  @Output() submit = new EventEmitter<any>();

  applicationForm: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder) {
    this.applicationForm = this.fb.group({
      propertyId: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      message: [''],
      requestBackground: [true],
      requestCredit: [true]
    });
  }

  handleSubmit(): void {
    if (this.applicationForm.valid) {
      this.loading = true;
      this.submit.emit(this.applicationForm.value);
    }
  }
}