import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { HomeRoutingModule } from './home-routing.module';
import { AboutComponent } from './pages/about/about.component';
import { RegisterComponent } from './pages/register/register.component';
import { FormsModule } from '@angular/forms';
import { HomeComponent } from './pages/home/home.component';
import { LoginComponent } from './pages/login/login.component'; 


@NgModule({
  declarations: [
    AboutComponent,
    RegisterComponent,
    HomeComponent,
    LoginComponent 
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    FormsModule,
  ]
})
export class HomeModule { }
