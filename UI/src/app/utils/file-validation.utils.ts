import { FileValidationResult, FileValidationOptions } from '../models/file.types';

const DEFAULT_OPTIONS: FileValidationOptions = {
  maxSize: 5 * 1024 * 1024, // 5MB
  allowedTypes: ['image/jpeg', 'image/png', 'image/gif', 'application/pdf']
};

export function validateFile(
  file: File, 
  options: FileValidationOptions = {}
): FileValidationResult {
  const { maxSize, allowedTypes } = { ...DEFAULT_OPTIONS, ...options };

  if (maxSize && file.size > maxSize) {
    return { 
      isValid: false, 
      error: `File size must be less than ${maxSize / (1024 * 1024)}MB` 
    };
  }
  
  if (allowedTypes && !allowedTypes.includes(file.type)) {
    return { 
      isValid: false, 
      error: 'Please upload an image or PDF file' 
    };
  }

  return { isValid: true };
}