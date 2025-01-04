import { Component } from '@angular/core';
import { FormGroup, Validators, ReactiveFormsModule, FormControl } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { FormFieldComponent } from '../../../../shared/components/form-field/form-field.component';
import { ButtonComponent } from '../../../../shared/components/button/button.component';
import { passwordValidator } from '../../utils/form-validators';
import { getFieldErrorMessage } from '../../utils/error-messages';

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
    password: new FormControl('', [Validators.required, passwordValidator]),
    rememberMe:new FormControl (false)

  });


  showPassword = false;
  loading = false;
  error = '';


  

  constructor(private authService: AuthService) 
  
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



  
  onSubmit(loginForm:FormGroup): void {


    console.log(loginForm.value);
    if (loginForm.valid) {
      this.loading = true;
      this.error = '';
      
      this.authService.login(loginForm.value)
      .subscribe({
        next: (response) => {
          if (response.success) {
            // TODO: Navigate to dashboard
            console.log('Login successful');
          } else {
            this.error = response.error || 'Login failed';
          }
        },
        error: (err) => {
          this.error = 'An unexpected error occurred. Please try again.';
          console.error('Login error:', err);
        },
        complete: () => {
          this.loading = false;
        }
      });
    }
  }
}