export function getFieldErrorMessage(field: string, errors: any): string {
  if (errors.required) {
    return `${field.charAt(0).toUpperCase() + field.slice(1)} is required`;
  }
  
  if (errors.email) {
    return 'Please enter a valid email address';
  }
  
  if (errors.minlength) {
    return `${field.charAt(0).toUpperCase() + field.slice(1)} must be at least ${errors.minlength.requiredLength} characters`;
  }

  if (errors.pattern) {
    return `Invalid ${field.toLowerCase()} format`;
  }

  return '';
}