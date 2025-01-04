export interface Receipt {
  id: string;
  file: File;
  name: string;
  size: number;
  type: string;
}

export interface ReceiptValidation {
  isValid: boolean;
  error?: string;
}