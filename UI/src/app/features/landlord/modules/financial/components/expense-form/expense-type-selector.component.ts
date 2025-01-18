import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';

@Component({
  selector: 'app-expense-type-selector',
  standalone: true,
  imports: [CommonModule],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: ExpenseTypeSelector,
      multi: true
    }
  ],
  template: `
    <div class="flex gap-4">
      <button
        type="button"
        [class]="getButtonClass('property')"
        (click)="selectType('property')"
      >
        <span class="material-icons mr-2">apartment</span>
        Property Expense
      </button>
      <button
        type="button"
        [class]="getButtonClass('general')"
        (click)="selectType('general')"
      >
        <span class="material-icons mr-2">account_balance_wallet</span>
        General Expense
      </button>
    </div>
  `
})
export class ExpenseTypeSelector implements ControlValueAccessor {
  selectedType: 'property' | 'general' = 'property';
  onChange = (value: string) => {};
  onTouched = () => {};

  getButtonClass(type: string): string {
    const baseClass = 'flex items-center px-4 py-2 rounded-lg border';
    return type === this.selectedType
      ? `${baseClass} bg-blue-50 border-blue-500 text-blue-700`
      : `${baseClass} border-gray-300 text-gray-700 hover:bg-gray-50`;
  }

  selectType(type: 'property' | 'general'): void {
    this.selectedType = type;
    this.onChange(type);
    this.onTouched();
  }

  writeValue(value: string): void {
    if (value) {
      this.selectedType = value as 'property' | 'general';
    }
  }

  registerOnChange(fn: any): void {
    this.onChange = fn;
  }

  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
}