import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { FormFieldComponent } from '../../../../../../../shared/components/form-field/form-field.component';
import { getFieldError } from '../utils/form-validation.utils';

@Component({
    selector: 'app-expense-details-form',
    imports: [CommonModule, ReactiveFormsModule, FormFieldComponent],
    template: `
    <div [formGroup]="formGroup" class="space-y-6">
      <app-form-field 
        label="Category" 
        id="category"
        [error]="getError('category')"
      >
        <select
          id="category"
          formControlName="category"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
        >
          <option value="">Select category</option>
          <option value="maintenance">Maintenance</option>
          <option value="utilities">Utilities</option>
          <option value="insurance">Insurance</option>
          <option value="taxes">Taxes</option>
          <option value="mortgage">Mortgage</option>
          <option value="acquisition">Acquisition</option>
        </select>
      </app-form-field>

      <app-form-field 
        label="Expense Type" 
        id="expenseType"
        [error]="getError('expenseType')"
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

      <div class="grid grid-cols-2 gap-4">
        <app-form-field 
          label="Due Date" 
          id="dueDate"
          [error]="getError('dueDate')"
        >
          <input
            type="date"
            id="dueDate"
            formControlName="dueDate"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          >
        </app-form-field>

        <app-form-field 
          label="Amount" 
          id="amount"
          [error]="getError('amount')"
        >
          <input
            type="number"
            id="amount"
            formControlName="amount"
            class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
            min="0"
            step="0.01"
          >
        </app-form-field>
      </div>

      <app-form-field 
        label="Details" 
        id="details"
        [error]="getError('details')"
      >
        <textarea
          id="details"
          formControlName="details"
          rows="3"
          class="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
          placeholder="Add any additional details..."
        ></textarea>
      </app-form-field>

      <div class="flex items-center gap-2">
        <input
          type="checkbox"
          id="isPaid"
          formControlName="isPaid"
          class="h-4 w-4 rounded border-gray-300 text-blue-600"
        >
        <label for="isPaid" class="text-sm text-gray-700">
          Mark as paid
        </label>
      </div>
    </div>
  `
})
export class ExpenseDetailsFormComponent {
  @Input() formGroup!: FormGroup;

  getError(fieldName: string): string {
    return getFieldError(this.formGroup.get(fieldName), fieldName);
  }
}