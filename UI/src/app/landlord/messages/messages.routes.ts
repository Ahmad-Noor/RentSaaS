import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./messages.page').then(m => m.MessagesPage)
  }
] as Routes;