import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/maintenance/maintenance.page').then(m => m.MaintenancePage)
  }
] as Routes;