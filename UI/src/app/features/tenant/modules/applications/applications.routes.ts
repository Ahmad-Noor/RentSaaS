import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/applications/applications.page').then(m => m.ApplicationsPage)
  }
] as Routes;