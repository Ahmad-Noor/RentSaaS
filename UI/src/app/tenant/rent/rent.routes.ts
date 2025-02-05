import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./rent/rent.page').then(m => m.RentPage)
  }
] as Routes;