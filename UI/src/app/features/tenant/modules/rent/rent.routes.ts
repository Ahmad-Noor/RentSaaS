import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/rent/rent.page').then(m => m.RentPage)
  }
] as Routes;