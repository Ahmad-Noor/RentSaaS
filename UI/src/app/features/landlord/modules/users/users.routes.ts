import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/users-list/users-list.page').then(m => m.UsersListPage)
  }
] as Routes;