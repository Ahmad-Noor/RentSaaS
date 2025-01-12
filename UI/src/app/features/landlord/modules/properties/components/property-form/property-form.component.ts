import { Component, EventEmitter, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-property-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, FormFieldComponent],
  templateUrl: './property-form.component.html',
  styleUrls: ['./property-form.component.css']
})
export class PropertyFormComponent {
  @Output() onSubmit = new EventEmitter<any>();






  propertyForm= new FormGroup({
    note:new FormControl('',[Validators.required,Validators.minLength(10)]),
    address:new FormControl('',[Validators.required,Validators.minLength(10)]),
  })

  
  loading = false;

  constructor() {

  }

  getFieldError(field: string): string {
    const control = this.propertyForm.get(field);
    if (control?.touched && control.errors) {
      if (control.errors['required']) {
        return `${field.charAt(0).toUpperCase() + field.slice(1)} is required`;
      }
    }
    return '';
  }

  handleSubmit(): void {
    if (this.propertyForm.valid) {
      this.loading = true;
      this.onSubmit.emit(this.propertyForm.value);
    }
  }
}