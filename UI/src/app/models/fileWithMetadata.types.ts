export interface FileWithMetadata {
  id: string;
  name: string;
  size: number;
  type: string;
  file: File;
}

export interface FileWithMetadataValidation {
  isValid: boolean;
  error?: string;
}