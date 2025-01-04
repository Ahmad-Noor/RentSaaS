export interface TaxPayment {
  id: number;
  date: string;
  type: string;
  property: string;
  amount: number;
  status: 'paid' | 'pending' | 'overdue';
}

export interface TaxDocument {
  id: number;
  name: string;
  year: string;
  type: 'pdf' | 'excel';
  category: 'income' | 'property' | 'deductions' | 'payments' | 'depreciation' | 'returns';
}