import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./maintenance/maintenance.page').then(m => m.MaintenancePage)
  }
] as Routes;