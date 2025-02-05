import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./Company-Page/companies.page').then(m => m.CompaniesPage)
  },
  {
    path: 'new',
    loadComponent: () => import('./company-form/company-form.page').then(m => m.CompanyFormPage)
  },
  // {
  //   path: ':id/edit',
  //   loadComponent: () => import('./pages/companies/company-form/company-form.page').then(m => m.CompanyFormPage)
  // }
] as Routes;