import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
    selector: 'app-tenant-nav',
    imports: [RouterLink, RouterLinkActive],
    template: `
    <nav class="bg-white shadow-sm">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between h-16">
          <div class="flex space-x-8">
            <a routerLink="applications" 
               routerLinkActive="text-blue-600 border-b-2 border-blue-600"
               class="inline-flex items-center px-1 pt-1 text-sm font-medium">
              Applications
            </a>
            <a routerLink="rent" 
               routerLinkActive="text-blue-600 border-b-2 border-blue-600"
               class="inline-flex items-center px-1 pt-1 text-sm font-medium">
              Pay Rent
            </a>
            <a routerLink="maintenance" 
               routerLinkActive="text-blue-600 border-b-2 border-blue-600"
               class="inline-flex items-center px-1 pt-1 text-sm font-medium">
              Maintenance
            </a>
            <a routerLink="messages" 
               routerLinkActive="text-blue-600 border-b-2 border-blue-600"
               class="inline-flex items-center px-1 pt-1 text-sm font-medium">
              Messages
            </a>
          </div>
        </div>
      </div>
    </nav>
  `
})
export class TenantNavComponent {}