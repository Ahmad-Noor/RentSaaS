import { ExpenseCategory, ExpenseType } from '../../../types/expense.types';

export interface ExpenseFormData {
  type: ExpenseType;
  propertyId?: string;
  category: ExpenseCategory;
  expenseType: 'recurring' | 'onetime' | 'scheduled';
  dueDate: string;
  amount: number;
  details?: string;
  receipts: File[];
  isPaid: boolean;
}