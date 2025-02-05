export const getFieldErrorMessage = (field: string, errors: any): string => {
  if (!errors) return '';

  if (errors['required']) {
    return `${field.charAt(0).toUpperCase() + field.slice(1)} is required`;
  }
  
  if (errors['email']) {
    return 'Please enter a valid email address';
  }
  
  if (errors['password']) {
    const { hasNumber, hasMinLength } = errors['password'];
    if (!hasMinLength) return 'Password must be at least 6 characters';
    if (!hasNumber) return 'Password must contain at least one number';
  }

  return '';
};