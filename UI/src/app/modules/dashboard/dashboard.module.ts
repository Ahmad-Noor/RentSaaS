import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { DashboardRoutingModule } from './dashboard-routing.module';
import { FormsModule } from '@angular/forms';
import {  provideClientHydration } from '@angular/platform-browser';
import {  provideHttpClient, withFetch } from '@angular/common/http';
import { ProjectsComponent } from './pages/projects/projects.component';
import { LayoutComponent } from './pages/layout/layout.component'; 
import { UsersComponent } from './pages/users/users.component';


@NgModule({ 
   declarations: [
    ProjectsComponent, 
    LayoutComponent, 
    UsersComponent
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule, 
    FormsModule
     
  ],
  providers: [
    provideClientHydration(), provideHttpClient(withFetch()), 
  ],
})
export class DashboardModule { }
