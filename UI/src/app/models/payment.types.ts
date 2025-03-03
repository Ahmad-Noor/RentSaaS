import { FileWithMetadata } from './fileWithMetadata.types';
export type PaymentCategory = 'maintenance' | 'utilities' | 'insurance' | 'taxes' | 'mortgage' | 'acquisition';

export type PaymentStatus = 'completed' | 'pending' | 'failed';
export type PaymentType = 'rent' | 'deposit' | 'fee' | 'other';

export interface Payment {
  id: string;
  date: string;
  property: string;
  description: string;
  amount: number;
  status: PaymentStatus;
  type: PaymentType;
  tenant?: string;
  reference?: string;
    dueDate?: string;
    files?: FileWithMetadata[];
    details?: string;
      category: PaymentCategory;
    
}
