import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/maintenance/maintenance.page').then(m => m.MaintenancePage)
  },
  {
    path: 'create',
    loadComponent: () => import('./pages/create-request/create-request.page').then(m => m.CreateRequestPage)
  }
] as Routes;