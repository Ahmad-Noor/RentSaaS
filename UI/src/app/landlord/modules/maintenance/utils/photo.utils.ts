import { FileValidation } from '../../../../shared/types/file.types';

const MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
const VALID_TYPES = ['image/jpeg', 'image/png', 'image/gif'];

export function validatePhoto(file: File): FileValidation {
  if (file.size > MAX_FILE_SIZE) {
    return { isValid: false, error: 'File size must be less than 5MB' };
  }
  
  if (!VALID_TYPES.includes(file.type)) {
    return { isValid: false, error: 'Please upload a valid image file (JPEG, PNG, or GIF)' };
  }

  return { isValid: true };
}