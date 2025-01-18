export interface FileValidation {
  isValid: boolean;
  error?: string;
}

export interface FileValidationOptions {
  maxSize?: number;
  allowedTypes?: string[];
}