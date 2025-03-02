import { FileWithMetadata } from './fileWithMetadata.types';

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
    receipts?: FileWithMetadata[];
}

export interface CreatePaymentDTO {
  property: string;
  type: PaymentType;
  amount: number;
  description: string;
  tenant?: string;
    receipts?: FileWithMetadata[];
}