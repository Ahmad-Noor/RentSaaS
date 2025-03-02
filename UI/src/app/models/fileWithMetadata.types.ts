export interface FileWithMetadata {
  id: string;
  name: string;
  size: number;
  type: string;
  file: File;
  url?: string;
}

export interface FileWithMetadataValidation {
  isValid: boolean;
  error?: string;
}