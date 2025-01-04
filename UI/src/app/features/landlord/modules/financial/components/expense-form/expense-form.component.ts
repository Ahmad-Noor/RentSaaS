import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ExpenseDetailsFormComponent } from './expense-details-form/expense-details-form.component';
import { ExpenseTypeSelectorComponent } from './expense-type-selector/expense-type-selector.component';
import { PropertySelectorComponent } from './property-selector/property-selector.component';
import { ReceiptUploadComponent } from './receipt-upload/receipt-upload.component';
import { Expense } from '../../types/expense.types';
import { ExpenseFormData } from './models/expense-form.model';
import { initializeExpenseForm } from './utils/form-utils';

@Component({
  selector: 'app-expense-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink,
    ExpenseDetailsFormComponent,
    ExpenseTypeSelectorComponent,
    PropertySelectorComponent,
    ReceiptUploadComponent
  ],
  template: `
    <form [formGroup]="expenseForm" (ngSubmit)="handleSubmit()" class="space-y-6">
      <app-expense-type-selector [formGroup]="expenseForm" />
      
      @if (expenseForm.get('type')?.value === 'property') {
        <app-property-selector [formGroup]="expenseForm" />
      }

      <app-expense-details-form [formGroup]="expenseForm" />
      
      <app-receipt-upload [formGroup]="expenseForm" />

      <div class="flex justify-end gap-4">
        <a 
          routerLink=".."
          class="px-4 py-2 text-gray-700 hover:text-gray-900"
        >
          Cancel
        </a>
        <button
          type="submit"
          [disabled]="!expenseForm.valid || loading"
          class="px-4 py-2 text-white bg-blue-600 rounded hover:bg-blue-700 disabled:opacity-50"
        >
          {{ loading ? 'Saving...' : 'Save Expense' }}
        </button>
      </div>
    </form>
  `
})
export class ExpenseFormComponent implements OnInit {
  @Input() expense?: Expense;
  @Output() save = new EventEmitter<ExpenseFormData>();
  
  expenseForm: FormGroup;
  loading = false;

  constructor(private fb: FormBuilder) {
    this.expenseForm = initializeExpenseForm(fb);
  }

  ngOnInit() {
    if (this.expense) {
      this.expenseForm.patchValue({
        type: this.expense.type || 'property',
        propertyId: this.expense.propertyId,
        category: this.expense.category,
        expenseType: this.expense.recurring ? 'recurring' : 'onetime',
        amount: this.expense.amount,
        dueDate: this.expense.dueDate,
        details: this.expense.description,
        isPaid: this.expense.status === 'paid'
      });
    }
  }

  handleSubmit(): void {
    if (this.expenseForm.valid) {
      this.loading = true;
      const formData: ExpenseFormData = {
        type: this.expenseForm.value.type,
        propertyId: this.expenseForm.value.propertyId,
        category: this.expenseForm.value.category,
        expenseType: this.expenseForm.value.expenseType,
        amount: this.expenseForm.value.amount,
        dueDate: this.expenseForm.value.dueDate,
        details: this.expenseForm.value.details,
        receipts: this.expenseForm.value.receipts || [],
        isPaid: this.expenseForm.value.isPaid
      };
      this.save.emit(formData);
    }
  }
}