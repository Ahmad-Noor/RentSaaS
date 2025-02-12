import { FormBuilder, Validators } from '@angular/forms';
import { ExpenseFormData } from '../models/expense-form.model'; 
import { CreateExpenseDTO } from '../../../types/expense.types';

export function initializeExpenseForm(fb: FormBuilder) {
  return fb.group({
    type: ['property', Validators.required],
    propertyId: [''],
    category: ['', Validators.required],
    expenseType: ['onetime', Validators.required],
    amount: ['', [Validators.required, Validators.min(0)]],
    dueDate: ['', Validators.required],
    details: [''],
    receipts: [[]],
    isPaid: [false]
  });
}

export function mapFormDataToDTO(formData: ExpenseFormData): CreateExpenseDTO {
  return {
    description: formData.details || '',
    amount: formData.amount,
    category: formData.category,
    date: new Date().toISOString(),
    dueDate: formData.dueDate,
    propertyId: formData.type === 'property' ? Number(formData.propertyId) : undefined,
    recurring: formData.expenseType === 'recurring',
    type: formData.type
  };
}