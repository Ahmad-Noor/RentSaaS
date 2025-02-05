import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./forms.page').then(m => m.FormsPage)
  }
] as Routes;