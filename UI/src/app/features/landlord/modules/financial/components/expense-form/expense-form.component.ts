import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ExpenseDetailsFormComponent } from './expense-details-form/expense-details-form.component';
import { ExpenseTypeSelectorComponent } from './expense-type-selector/expense-type-selector.component';
import { PropertySelectorComponent } from './property-selector/property-selector.component';
import { VendorSelectorComponent } from './vendor-selector/vendor-selector.component';
import { ExpenseFormData } from './models/expense-form.model';
import { Expense } from '../../types/expense.types';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-expense-form',
    imports: [
        CommonModule,
        ReactiveFormsModule,
        RouterLink,
        ExpenseDetailsFormComponent,
        ExpenseTypeSelectorComponent,
        PropertySelectorComponent,
        VendorSelectorComponent
    ],
    template: `
    <form [formGroup]="expenseForm" (ngSubmit)="onSubmit()" class="space-y-6">
      <app-expense-type-selector [formGroup]="expenseForm" />
      
      @if (expenseForm.get('type')?.value === 'property') {
        <app-property-selector [formGroup]="expenseForm" />
      }

      <app-vendor-selector [formGroup]="expenseForm" />
      
      <app-expense-details-form [formGroup]="expenseForm" />

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
  @Output() onSave = new EventEmitter<ExpenseFormData>();
  
  loading = false;
  expenseForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.expenseForm = this.fb.group({
      type: ['property', Validators.required],
      propertyId: [''],
      vendor: [''],
      category: ['', Validators.required],
      expenseType: ['onetime', Validators.required],
      dueDate: ['', Validators.required],
      amount: ['', [Validators.required, Validators.min(0)]],
      details: [''],
      receipts: [[]],
      isPaid: [false]
    });

    this.setupPropertyValidation();
  }

  ngOnInit(): void {
    if (this.expense) {
      this.expenseForm.patchValue({
        type: this.expense.type || 'property',
        propertyId: this.expense.propertyId,
        vendor: this.expense.vendor,
        category: this.expense.category,
        expenseType: this.expense.recurring ? 'recurring' : 'onetime',
        dueDate: this.expense.dueDate,
        amount: this.expense.amount,
        details: this.expense.description,
        isPaid: this.expense.status === 'paid'
      });
    }
  }

  private setupPropertyValidation(): void {
    this.expenseForm.get('type')?.valueChanges.subscribe(type => {
      const propertyId = this.expenseForm.get('propertyId');
      if (type === 'property') {
        propertyId?.setValidators(Validators.required);
      } else {
        propertyId?.clearValidators();
      }
      propertyId?.updateValueAndValidity();
    });
  }

  onSubmit(): void {
    if (this.expenseForm.valid) {
      this.loading = true;
      this.onSave.emit(this.expenseForm.value);
    }
  }
}