import { Component } from '@angular/core';

@Component({
  selector: 'app-applications-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">Rental Applications</h1>
      <div class="bg-white rounded-lg shadow p-4">
        <div class="space-y-4">
          <div class="border-b pb-4">
            <h2 class="text-lg font-semibold">Active Applications</h2>
          </div>
          <!-- Application list -->
        </div>
      </div>
    </div>
  `
})
export class ApplicationsPage {}