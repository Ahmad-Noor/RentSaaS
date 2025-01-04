import { Routes } from '@angular/router';
import { TenantPortalPage } from './pages/tenant-portal/tenant-portal.page';

export default [
  {
    path: '',
    component: TenantPortalPage,
    children: [
      {
        path: 'applications',
        loadChildren: () => import('./modules/applications/applications.routes')
      },
      {
        path: 'rent',
        loadChildren: () => import('./modules/rent/rent.routes')
      },
      {
        path: 'maintenance',
        loadChildren: () => import('./modules/maintenance/maintenance.routes')
      },
      {
        path: 'messages',
        loadChildren: () => import('./modules/messages/messages.routes')
      }
    ]
  }
] as Routes;