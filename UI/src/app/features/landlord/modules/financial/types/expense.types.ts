export type ExpenseCategory = 'maintenance' | 'utilities' | 'insurance' | 'taxes' | 'mortgage' | 'acquisition';
export type ExpenseStatus = 'paid' | 'pending' | 'overdue';
export type ExpenseType = 'property' | 'general';

export interface Expense {
  id: number;
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

export interface CreateExpenseDTO {
  description: string;
  amount: number;
  category: ExpenseCategory;
  date: string;
  propertyId?: number;
  recurring?: boolean;
  dueDate?: string;
  vendor?: string;
  type?: ExpenseType;
  reference?: string;
}