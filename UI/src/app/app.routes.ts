import { Routes } from '@angular/router';
import { HomePage } from './home/pages/home/home.page';

export const routes: Routes = [
  {
    path: '',
    component: HomePage
  },
  {
    path: 'pricing',
    loadComponent: () => import('./home/pricing/pages/pricing/pricing.page').then(m => m.PricingPage)
  },
  {
    path: 'login',
    loadComponent: () => import('./auth/pages/login/login.page').then(m => m.LoginPage)
  },
  {
    path: 'register',
    loadComponent: () => import('./auth/pages/register/register.page').then(m => m.RegisterPage)
  },
  {
    path: 'landlord',
    loadChildren: () => import('./landlord/landlord.routes').then(m => m.default)
  },
  {
    path: 'tenant',
    loadChildren: () => import('./tenant/tenant.routes')
  },
  {
    path: '**',
    redirectTo: ''
  }
];