// import { AbstractControl, ValidationErrors } from '@angular/forms';

// export function passwordValidator(control: AbstractControl): ValidationErrors | null {
//   const value = control.value;
  
//   if (!value) {
//     return null;
//   }

//   const hasNumber = /[0-9]/.test(value);
//   const hasMinLength = value.length >= 6;

//   if (!hasNumber || !hasMinLength) {
//     return {
//       password: {
//         hasNumber,
//         hasMinLength
//       }
//     };
//   }

//   return null;
// }