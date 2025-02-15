import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginFormComponent } from '../login-form/login-form.component';
import { SocialLoginComponent } from '../social-login/social-login.component';
import { AuthLayoutComponent } from '../layouts/auth-layout/auth-layout.component';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
    selector: 'app-login-page',
    standalone:true,
    imports: [RouterLink, LoginFormComponent, SocialLoginComponent, AuthLayoutComponent,ReactiveFormsModule],
    templateUrl:'./login.pages.html'  ,
    styleUrl:'./login.pages.css',

})
export class LoginPage { }