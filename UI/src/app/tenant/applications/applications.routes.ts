import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./applications.page').then(m => m.ApplicationsPage)
  }
] as Routes;