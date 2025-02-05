import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router'; 
import { TenantNavComponent } from '../tenant-nav/tenant-nav.component';

@Component({
    selector: 'app-tenant-portal',
    imports: [RouterOutlet, TenantNavComponent],
    template: `
    <div class="min-h-screen bg-gray-100">
      <app-tenant-nav />
      
      <main class="py-10">
        <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <router-outlet></router-outlet>
        </div>
      </main>
    </div>
  `
})
export class TenantPortalPage {}