export type ExpenseCategory = 'maintenance' | 'utilities' | 'insurance' | 'taxes' | 'mortgage' | 'acquisition';
export type ExpenseStatus = 'paid' | 'pending' | 'overdue';
export type ExpenseType = 'property' | 'general';

export interface Expense {
  Id: string
  ExpenseType: string
  PaymentSchedule: string
  PropertyId: string
  Category: string
  Amount: number
  DueDate: string
  Details: string
  IsPaid: boolean
  ReceiptsFiles: string
  OrganizationId: string
  CreatedAt: string
  CreatedBy: string
  LastModifiedAt: string
  LastModifiedBy: string
  IsDeleted: boolean
  DeletedAt: any
  DeletedBy: any
  Note: string
  CompanyId: string
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