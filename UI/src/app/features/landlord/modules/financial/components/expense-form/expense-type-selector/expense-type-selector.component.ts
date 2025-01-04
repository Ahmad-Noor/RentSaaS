import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-expense-type-selector',
    imports: [CommonModule, ReactiveFormsModule],
    template: `
    <div [formGroup]="formGroup" class="space-y-4">
      <label class="block text-sm font-medium text-gray-700">Expense Type</label>
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
    </div>
  `
})
export class ExpenseTypeSelectorComponent {
  @Input() formGroup!: FormGroup;
}