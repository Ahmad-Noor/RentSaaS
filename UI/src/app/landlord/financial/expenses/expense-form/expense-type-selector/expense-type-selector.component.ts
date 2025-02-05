import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../shared/components/form-field/form-field.component';

@Component({
  selector: 'app-expense-type-selector',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
  template: `
    <div [formGroup]="formGroup" class="space-y-4">
      <!-- Property/General Type Selection -->
      <div class="flex gap-4">
        <label class="flex items-center p-4 border rounded-lg cursor-pointer hover:bg-gray-50"
               [class.border-blue-500]="formGroup.get('type')?.value === 'property'"
               [class.bg-blue-50]="formGroup.get('type')?.value === 'property'">
          <input
            type="radio"
            formControlName="type"
            value="property"
            class="hidden"
          >
          <span class="material-icons text-gray-600 mr-2">apartment</span>
          <span>Property Expense</span>
        </label>

        <label class="flex items-center p-4 border rounded-lg cursor-pointer hover:bg-gray-50"
               [class.border-blue-500]="formGroup.get('type')?.value === 'general'"
               [class.bg-blue-50]="formGroup.get('type')?.value === 'general'">
          <input
            type="radio"
            formControlName="type"
            value="general"
            class="hidden"
          >
          <span class="material-icons text-gray-600 mr-2">account_balance_wallet</span>
          <span>General Expense</span>
        </label>
      </div>

      <!-- Recurring/One-time Selection -->
      <app-form-field 
        label="Payment Schedule" 
        id="expenseType"
        [error]="getFieldError('expenseType')"
      >
        <select
          id="expenseType"
          formControlName="expenseType"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
        >
          <option value="onetime">One-time</option>
          <option value="recurring">Recurring</option>
          <option value="scheduled">Scheduled</option>
        </select>
      </app-form-field>
    </div>
  `
})
export class ExpenseTypeSelectorComponent {
  @Input() formGroup!: FormGroup;

  getFieldError(field: string): string {
    const control = this.formGroup.get(field);
    if (control?.touched && control.errors) {
      if (control.errors['required']) {
        return `${field} is required`;
      }
    }
    return '';
  }
}