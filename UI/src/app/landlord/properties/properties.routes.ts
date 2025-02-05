import { Routes } from '@angular/router';

export default [
  {
    path: '',
    loadComponent: () => import('./properties-list/properties-list.page').then(m => m.PropertiesListPage)
  },
  {
    path: 'new',
    loadComponent: () => import('./add-property/add-property.page').then(m => m.AddPropertyPage)
  },
  {
    path: 'advertising',
    loadComponent: () => import('./advertising/advertising.page').then(m => m.AdvertisingPage)
  },
  {
    path: 'advertising/create',
    loadComponent: () => import('./create-listing/create-listing.page').then(m => m.CreateListingPage)
  },
  {
    path: 'applications',
    loadComponent: () => import('./applications/applications.page').then(m => m.ApplicationsPage)
  },
  {
    path: 'applications/send',
    loadComponent: () => import('./send-application/send-application.page').then(m => m.SendApplicationPage)
  },
  {
    path: 'lease',
    loadComponent: () => import('./lease/lease.page').then(m => m.LeasePage)
  },
  {
    path: 'lease/create',
    loadComponent: () => import('./create-lease/create-lease.page').then(m => m.CreateLeasePage)
  }
] as Routes;