import { FileWithMetadataValidation } from '../models/fileWithMetadata.types';

const MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
const VALID_TYPES = ['image/jpeg', 'image/png', 'image/gif', 'application/pdf'];

export function validateReceipt(file: File): FileWithMetadataValidation {
  if (file.size > MAX_FILE_SIZE) {
    return { isValid: false, error: 'File size must be less than 5MB' };
  }
  
  if (!VALID_TYPES.includes(file.type)) {
    return { isValid: false, error: 'Please upload an image or PDF file' };
  }

  return { isValid: true };
}