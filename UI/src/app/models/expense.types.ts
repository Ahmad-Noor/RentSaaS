export type ExpenseCategory = 'maintenance' | 'utilities' | 'insurance' | 'taxes' | 'mortgage' | 'acquisition';
export type ExpenseStatus = 'paid' | 'pending' | 'overdue';
export type ExpenseType = 'property' | 'general';

export interface Expense {
  id: string;
  date: string;
  description: string;
  amount: number;
  category: ExpenseCategory;
  status: ExpenseStatus;
  propertyId?: number;
  recurring?: boolean;
  dueDate?: string;
  vendor?: string;
  type?: ExpenseType;
  reference?: string;
}
 