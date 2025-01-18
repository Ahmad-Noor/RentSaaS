import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/forms/forms.page').then(m => m.FormsPage)
  }
] as Routes;