import { Component } from '@angular/core';
import { FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/auth.service';
import { FormFieldComponent } from '../../shared/components/form-field/form-field.component';
import { ButtonComponent } from '../../shared/components/button/button.component';
// import { passwordValidator } from '../../utils/form-validators';
import { getFieldErrorMessage } from '../utils/error-messages';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';
import { Constant } from "../../constants";



@Component({
    selector: 'app-login-form',
    imports: [CommonModule, ReactiveFormsModule, FormFieldComponent, ButtonComponent],
    standalone: true,
    templateUrl: './login-form.component.html',
    styleUrls: ['./login-form.component.css']
})
export class LoginFormComponent {


  loginForm=new  FormGroup({
    email: new FormControl  ('',[Validators.required, Validators.email]),
    password: new FormControl('', [Validators.required]),
    rememberMe:new FormControl (false)

  });


  showPassword = false;
  loading = false;
  error = '';


  

  constructor(private authService: AuthService ,private _router:Router) 
  
  {

  }



  

  togglePassword(): void {
    this.showPassword = !this.showPassword;
  }

  getFieldError(field: string): string {
    const control = this.loginForm.get(field);
    if (control?.touched && control.errors) {
      return getFieldErrorMessage(field, control.errors);
    }
    return '';
  }



  
  onSubmit(loginForm: FormGroup): void {
    console.log('Form Value:', loginForm.value);
    console.log('Form Valid:', loginForm.valid);
  
    if (loginForm.valid) {
      this.loading = true;
      this.error = '';
  
      this.authService.login(loginForm.value).subscribe({
        next: (response) => {
          try {
            console.log('Login Response:', response);
  
            if (!response.token) throw new Error('Token is missing in the response.');
  
            const jwtToken = jwtDecode(response.token);

            localStorage.setItem(Constant.token, response.token);

            localStorage.setItem(Constant.OrganizationIdRentSass, response.organizationId || '');
            this.authService.SaveData();
  
            this.navigateBasedOnUserType(response.userType);
          } catch (errorExceptions:any) {
            console.error('Error processing login response:', errorExceptions.message);
            this.error = 'An unexpected error occurred. Please try again.';
          }
        },
        error: (err) => {
          this.error = err.error.message || 'Login failed. Please try again.';
          console.error('Login Error:', err);
          this.loading = false;
        },
        complete: () => {
          this.loading = false;
        },
      });
    } else {
      this.error = 'Please fill out the form correctly before submitting.';
    }
  }
  
  navigateBasedOnUserType(userType: string): void {
    if (!userType) {
      console.error('User type is missing.');
      this.error = 'User type is missing. Please contact support.';
      return;
    }
  
    const userTypeLower = userType.toLowerCase();
    if (userTypeLower === 'tenant') {
      this._router.navigate(['/tenant']);
    } else if (userTypeLower === 'landlord') {
      this._router.navigate(['/landlord']);
    } else {
      console.warn(`Unhandled user type: ${userType}`);
      this.error = 'Unexpected user type. Please contact support.';
    }
  }

  


}

