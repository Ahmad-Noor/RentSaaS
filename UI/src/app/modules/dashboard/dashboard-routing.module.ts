import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProjectsComponent } from './pages/projects/projects.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { UsersComponent } from './pages/users/users.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';

const routes: Routes = [ 
  
  {
    path: 'dashboard',
    component: LayoutComponent,
    children: [
      { path: 'projects', component: ProjectsComponent },
      { path: 'users', component: UsersComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'projects', component: ProjectsComponent }
    ],
  },
  
  
  ];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
