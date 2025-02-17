import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component'; 
import { PropertySelectorComponent } from '../../property-selector/property-selector.component';

@Component({
  selector: 'app-application-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent, PropertySelectorComponent],
  templateUrl:"application-form.component.html"
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