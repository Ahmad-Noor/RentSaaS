import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { LoginFormComponent } from '../../components/login-form/login-form.component';
import { SocialLoginComponent } from '../../components/social-login/social-login.component';
import { AuthLayoutComponent } from '../../layouts/auth-layout/auth-layout.component';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
    selector: 'app-login-page',
    standalone:true,
    imports: [RouterLink, LoginFormComponent, SocialLoginComponent, AuthLayoutComponent,ReactiveFormsModule],
    templateUrl:'./login.pages.html'  ,
    styleUrl:'./login.pages.css',

})
export class LoginPage {









}