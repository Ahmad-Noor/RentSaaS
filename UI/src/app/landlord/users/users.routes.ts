import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/users-list/users-list.page').then(m => m.UsersListPage)
  },
  {
    path: 'add',
    loadComponent: () => import('./pages/add-user/add-user.page').then(m => m.AddUserPage)
  }
] as Routes;