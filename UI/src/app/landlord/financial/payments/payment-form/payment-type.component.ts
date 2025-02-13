import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms'; 
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-payment-type',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <app-form-field label="Payment Type" id="type">
      <select
        id="type"
        formControlName="type"
        class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
      >
        <option value="">Select type</option>
        <option value="rent">Rent</option>
        <option value="deposit">Security Deposit</option>
        <option value="fee">Fee</option>
        <option value="other">Other</option>
      </select>
    </app-form-field>
  `
})
export class PaymentTypeComponent {
  @Input() formGroup!: FormGroup;
}