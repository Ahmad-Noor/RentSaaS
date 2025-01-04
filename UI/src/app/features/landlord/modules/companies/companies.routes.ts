import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/companies-list/companies-list.page').then(m => m.CompaniesListPage)
  },
  {
    path: 'new',
    loadComponent: () => import('./pages/company-form/company-form.page').then(m => m.CompanyFormPage)
  },
  {
    path: ':id/edit',
    loadComponent: () => import('./pages/company-form/company-form.page').then(m => m.CompanyFormPage)
  }
] as Routes;