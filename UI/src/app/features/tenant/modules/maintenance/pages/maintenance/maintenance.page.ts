import { Component } from '@angular/core';

@Component({
  selector: 'app-maintenance-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">Maintenance Requests</h1>
      <div class="grid grid-cols-1 gap-4">
        <div class="bg-white rounded-lg shadow p-4">
          <h2 class="text-lg font-semibold mb-4">Submit Request</h2>
          <!-- Request form -->
        </div>
        <div class="bg-white rounded-lg shadow p-4">
          <h2 class="text-lg font-semibold mb-4">Request History</h2>
          <!-- Request history -->
        </div>
      </div>
    </div>
  `
})
export class MaintenancePage {}