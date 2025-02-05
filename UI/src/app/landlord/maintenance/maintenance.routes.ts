import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./maintenance/maintenance.page').then(m => m.MaintenancePage)
  },
  {
    path: 'create',
    loadComponent: () => import('./create-request/create-request.page').then(m => m.CreateRequestPage)
  }
] as Routes;