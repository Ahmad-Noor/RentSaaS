import { Routes } from '@angular/router';
import { register } from 'module';
import { HomeComponent } from './Pages/home/home.component';
import { LoginComponent } from './Pages/login/login.component';
import { RegisterComponent } from './Pages/register/register.component';
register


export const routes: Routes = [

    { path: '', component: HomeComponent },
    { path: 'login', component: LoginComponent },
    { path: 'Register', component: RegisterComponent },

];


 
 
