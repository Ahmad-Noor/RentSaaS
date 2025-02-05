import { Routes } from '@angular/router';
import { TenantPortalPage } from './tenant-portal/tenant-portal.page';

export default [
  {
    path: '',
    component: TenantPortalPage,
    children: [
      {
        path: 'applications',
        loadChildren: () => import('./applications/applications.routes')
      },
      {
        path: 'rent',
        loadChildren: () => import('./rent/rent.routes')
      },
      {
        path: 'maintenance',
        loadChildren: () => import('./maintenance/maintenance.routes')
      },
      {
        path: 'messages',
        loadChildren: () => import('./messages/messages.routes')
      }
    ]
  }
] as Routes;