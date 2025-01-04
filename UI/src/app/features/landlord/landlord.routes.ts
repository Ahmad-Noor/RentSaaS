import { Routes } from '@angular/router';
import { LandlordPortalPage } from './pages/landlord-portal/landlord-portal.page';

export default [
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
        loadComponent: () => import('./pages/dashboard/dashboard.page').then(m => m.DashboardPage)
      },
      {
        path: 'companies',
        loadChildren: () => import('./modules/companies/companies.routes')
      },
      {
        path: 'properties',
        loadChildren: () => import('./modules/properties/properties.routes')
      },
      {
        path: 'financial',
        loadChildren: () => import('./modules/financial/financial.routes')
      },
      {
        path: 'maintenance',
        loadChildren: () => import('./modules/maintenance/maintenance.routes')
      },
      {
        path: 'messages',
        loadChildren: () => import('./modules/messages/messages.routes')
      },
      {
        path: 'forms',
        loadChildren: () => import('./modules/forms/forms.routes')
      },
      {
        path: 'team',
        loadChildren: () => import('./modules/team/team.routes')
      },
      {
        path: 'users',
        loadChildren: () => import('./modules/users/users.routes')
      }
    ]
  }
] as Routes;