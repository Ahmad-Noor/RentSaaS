import { Component } from '@angular/core';
import { RequestListComponent } from '../../components/request-list/request-list.component';

@Component({
    selector: 'app-maintenance-page',
    imports: [RequestListComponent],
    template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Maintenance Requests</h1>
        <button class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors">
          <span class="material-icons text-sm">add</span>
          Create Request
        </button>
      </div>

      <app-request-list />
    </div>
  `
})
export class MaintenancePage {}