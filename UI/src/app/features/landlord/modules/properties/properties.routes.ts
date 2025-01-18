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
    path: 'advertising/create',
    loadComponent: () => import('./pages/create-listing/create-listing.page').then(m => m.CreateListingPage)
  },
  {
    path: 'applications',
    loadComponent: () => import('./pages/applications/applications.page').then(m => m.ApplicationsPage)
  },
  {
    path: 'applications/send',
    loadComponent: () => import('./pages/send-application/send-application.page').then(m => m.SendApplicationPage)
  },
  {
    path: 'lease',
    loadComponent: () => import('./pages/lease/lease.page').then(m => m.LeasePage)
  },
  {
    path: 'lease/create',
    loadComponent: () => import('./pages/create-lease/create-lease.page').then(m => m.CreateLeasePage)
  }
] as Routes;