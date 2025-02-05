import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./team-list/team-list.page').then(m => m.TeamListPage)
  }
] as Routes;