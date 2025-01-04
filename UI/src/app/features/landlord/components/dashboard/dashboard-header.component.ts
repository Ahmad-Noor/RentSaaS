import { Component } from '@angular/core';

@Component({
  selector: 'app-dashboard-header',
  standalone: true,
  template: `
    <div class="bg-white border-b">
      <div class="flex items-center justify-between p-4">
        <div class="flex items-center space-x-4">
          <h1 class="text-xl font-semibold">Dashboard</h1>
          <button class="text-sm text-blue-600 hover:text-blue-700">+ New dashboard</button>
          <button class="text-sm text-blue-600 hover:text-blue-700">Edit dashboard</button>
        </div>
        <div class="flex items-center space-x-4">
          <button class="text-sm text-blue-600 hover:text-blue-700">Share</button>
          <button class="text-sm text-blue-600 hover:text-blue-700">Fullscreen</button>
          <button class="text-sm text-blue-600 hover:text-blue-700">Clone</button>
          <button class="text-sm text-red-600 hover:text-red-700">Delete</button>
        </div>
      </div>
    </div>
  `
})
export class DashboardHeaderComponent {}