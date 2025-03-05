import { Routes } from '@angular/router';
import { LandlordPortalPage } from './dashboard/landlord-portal/landlord-portal.page';

export default [
  {
    path: '',
    component: LandlordPortalPage
  },
  {
    path: '',
    component: LandlordPortalPage,
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },
      {
        path: 'dashboard',
        loadComponent: () => import('./dashboard/dashboard.page').then(m => m.DashboardPage)
      },
      {
        path: 'companies',
        loadChildren: () => import('./companies/companies.routes')
      },
      {
        path: 'properties',
        loadChildren: () => import('./properties/properties.routes')
      },
      {
        path: 'financial',
        loadChildren: () => import('./financial/financial.routes')
      },


      {
        path: 'messages',
        loadChildren: () => import('./messages/messages.routes')
      },
      {
        path: 'forms',
        loadChildren: () => import('./forms/forms.routes')
      },
      {
        path: 'team',
        loadChildren: () => import('./team/team.routes')
      },
      {
        path: 'users',
        loadChildren: () => import('./users/users.routes')
      }
    ]
  }
] as Routes;