import { Component } from '@angular/core';
import { RouterLink } from '@angular/router'; 
import { RequestListComponent } from '../request-list/request-list.component';

@Component({
  selector: 'app-maintenance-page',
  standalone: true,
  imports: [RouterLink, RequestListComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Maintenance Requests</h1>
          <p class="mt-1 text-gray-600">Track and manage maintenance requests</p>
        </div>
        <a 
          routerLink="create"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Create Request
        </a>
      </div>

      <app-request-list />
    </div>
  `
})
export class MaintenancePage {}