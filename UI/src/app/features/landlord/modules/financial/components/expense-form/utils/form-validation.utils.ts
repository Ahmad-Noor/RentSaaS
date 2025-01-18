import { AbstractControl, ValidationErrors } from '@angular/forms';

export function getFieldError(control: AbstractControl | null, fieldName: string): string {
  if (control?.touched && control.errors) {
    if (control.errors['required']) {
      return `${fieldName.charAt(0).toUpperCase() + fieldName.slice(1)} is required`;
    }
    if (control.errors['min']) {
      return `${fieldName} must be greater than ${control.errors['min'].min}`;
    }
  }
  return '';
}