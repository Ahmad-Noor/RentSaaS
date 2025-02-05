import { FormBuilder, Validators } from '@angular/forms';

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