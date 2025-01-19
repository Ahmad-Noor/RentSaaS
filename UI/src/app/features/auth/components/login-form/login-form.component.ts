import { Component } from '@angular/core';
import { FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component';
import { ButtonComponent } from '../../../../shared/components/button/button.component';
// import { passwordValidator } from '../../utils/form-validators';
import { getFieldErrorMessage } from '../../utils/error-messages';
import { Router } from '@angular/router';
import { jwtDecode } from 'jwt-decode';




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
  
      this.authService.login(loginForm.value)
        .subscribe({
          next: (response) => {
            console.log('Login Response:', response);
      
              console.log('Login Successful:', response);
              const jwtToken = jwtDecode(response.token);
              console.log('Decoded JWT Token:', jwtToken);
              localStorage.setItem('token', response.token);
              localStorage.setItem('orgnaizationId', response.orgnaizationId);
              // this.authService.SaveData();
  
              if (response.userType === 'tenant') {
                this._router.navigate(['/tenant']);
              } else if (response.userType === 'landlord') {
                this._router.navigate(['/landlord']);
              }
          },
          error: (err) => {
            this.error = err.error.message;
            console.error('Login Error:', err);
            this.loading = false;
          },
          complete: () => {

            this.loading = false;
          }
        });
    }
  }
}

