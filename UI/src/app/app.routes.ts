import { Routes } from '@angular/router';
import { HomePage } from './features/home/pages/home/home.page';

export const routes: Routes = [
  {
    path: '',
    component: HomePage
  },
  {
    path: 'pricing',
    loadComponent: () => import('./features/pricing/pages/pricing/pricing.page').then(m => m.PricingPage)
  },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/pages/login/login.page').then(m => m.LoginPage)
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/pages/register/register.page').then(m => m.RegisterPage)
  },
  {
    path: 'landlord',
    loadChildren: () => import('./features/landlord/landlord.routes')
  },
  {
    path: 'tenant',
    loadChildren: () => import('./features/tenant/tenant.routes')
  },
  {
    path: '**',
    redirectTo: ''
  }
];