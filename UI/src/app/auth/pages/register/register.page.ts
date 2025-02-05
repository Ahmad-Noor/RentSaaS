import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { RegisterFormComponent } from '../../components/register-form/register-form.component';
import { SocialLoginComponent } from '../../components/social-login/social-login.component';
import { AuthLayoutComponent } from '../../layouts/auth-layout/auth-layout.component';

@Component({
    selector: 'app-register-page',
    imports: [RouterLink, RegisterFormComponent, SocialLoginComponent, AuthLayoutComponent],
    templateUrl:'./register.page.html',
    standalone: true,
    styleUrl: './register.page.css'
})
export class RegisterPage {}