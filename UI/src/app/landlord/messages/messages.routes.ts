import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/messages/messages.page').then(m => m.MessagesPage)
  }
] as Routes;