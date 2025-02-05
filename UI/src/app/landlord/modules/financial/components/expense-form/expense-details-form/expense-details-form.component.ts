import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-expense-details-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  templateUrl: './expense-details-form.component.html'
})
export class ExpenseDetailsFormComponent {
  @Input() formGroup!: FormGroup;

  getFieldError(field: string): string {
    const control = this.formGroup.get(field);
    if (control?.touched && control.errors) {
      if (control.errors['required']) {
        return `${field} is required`;
      }
      if (control.errors['min']) {
        return `${field} must be greater than ${control.errors['min'].min}`;
      }
    }
    return '';
  }





  
}