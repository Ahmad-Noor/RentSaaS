import { ExpenseCategory } from '../../../types/expense.types';

export interface ExpenseFormData {
  type: 'property' | 'general';
  propertyId?: string;
  vendor?: string;
  category: ExpenseCategory;
  expenseType: 'recurring' | 'onetime' | 'scheduled';
  dueDate: string;
  amount: number;
  details?: string;
  receipts: File[];
  isPaid: boolean;
}