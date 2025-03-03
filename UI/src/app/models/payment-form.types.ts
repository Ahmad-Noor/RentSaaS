import { PaymentStatus, PaymentType } from "./payment.types";

export interface PaymentFormData {
  type: PaymentType;
  propertyId?: string;
  status: PaymentStatus;
  expenseType: 'recurring' | 'onetime' | 'scheduled';
  dueDate: string;
  amount: number;
  details?: string;
  receipts: File[];
  isPaid: boolean;
}