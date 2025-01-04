import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./pages/properties-list/properties-list.page').then(m => m.PropertiesListPage)
  },
  {
    path: 'new',
    loadComponent: () => import('./pages/add-property/add-property.page').then(m => m.AddPropertyPage)
  },
  {
    path: 'advertising',
    loadComponent: () => import('./pages/advertising/advertising.page').then(m => m.AdvertisingPage)
  },
  {
    path: 'applications',
    loadComponent: () => import('./pages/applications/applications.page').then(m => m.ApplicationsPage)
  },
  {
    path: 'lease',
    loadComponent: () => import('./pages/lease/lease.page').then(m => m.LeasePage)
  }
] as Routes;