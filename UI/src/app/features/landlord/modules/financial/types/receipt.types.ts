export interface Receipt {
  id: string;
  file: File;
  name: string;
  size: number;
  type: string;
  progress?: number;
  error?: string;
}

export interface ReceiptValidation {
  isValid: boolean;
  error?: string;
}