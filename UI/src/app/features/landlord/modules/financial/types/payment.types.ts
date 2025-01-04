import { Receipt } from './receipt.types';

export type PaymentStatus = 'completed' | 'pending' | 'failed';
export type PaymentType = 'rent' | 'deposit' | 'fee' | 'other';

export interface Payment {
  id: number;
  date: string;
  property: string;
  description: string;
  amount: number;
  status: PaymentStatus;
  type: PaymentType;
  tenant?: string;
  reference?: string;
  receipts?: Receipt[];
}

export interface CreatePaymentDTO {
  property: string;
  type: PaymentType;
  amount: number;
  description: string;
  tenant?: string;
  receipts?: Receipt[];
}