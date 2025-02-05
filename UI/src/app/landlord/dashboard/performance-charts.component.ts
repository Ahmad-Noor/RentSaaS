import { Component } from '@angular/core';

@Component({
  selector: 'app-performance-charts',
  standalone: true,
  template: `
    <div class="grid grid-cols-2 gap-4 mb-6">
      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">CPU & Memory Usage</h3>
        <div class="h-48 flex items-center justify-center bg-gray-50 rounded">
          <span class="text-gray-400">Chart placeholder</span>
        </div>
        <div class="mt-4 grid grid-cols-2 gap-4">
          <div>
            <span class="text-sm text-gray-600">CPU Usage</span>
            <div class="text-xl font-semibold">45%</div>
          </div>
          <div>
            <span class="text-sm text-gray-600">Memory Usage</span>
            <div class="text-xl font-semibold">2.88 GB</div>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">Response Time & DTU</h3>
        <div class="h-48 flex items-center justify-center bg-gray-50 rounded">
          <span class="text-gray-400">Chart placeholder</span>
        </div>
        <div class="mt-4 grid grid-cols-2 gap-4">
          <div>
            <span class="text-sm text-gray-600">Avg Response</span>
            <div class="text-xl font-semibold">189.32 ms</div>
          </div>
          <div>
            <span class="text-sm text-gray-600">DTU Usage</span>
            <div class="text-xl font-semibold">32/55000</div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class PerformanceChartsComponent {}