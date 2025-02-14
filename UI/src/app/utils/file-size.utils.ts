const BYTES_PER_KB = 1024;

export const FileSizeUnits = ['Bytes', 'KB', 'MB'] as const;

export function formatFileSize(bytes: number): string {
  if (bytes === 0) return '0 Bytes';
  
  const i = Math.floor(Math.log(bytes) / Math.log(BYTES_PER_KB));
  const size = parseFloat((bytes / Math.pow(BYTES_PER_KB, i)).toFixed(2));
  
  return `${size} ${FileSizeUnits[i]}`;
}