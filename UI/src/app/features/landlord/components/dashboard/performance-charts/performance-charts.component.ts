import { Component } from '@angular/core';

@Component({
  selector: 'app-performance-charts',
  standalone: true,
  template: `
    <div class="grid grid-cols-2 gap-4">
      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">Revenue & Expenses</h3>
        <div class="h-48 flex items-center justify-center bg-gray-50 rounded">
          <span class="text-gray-400">Chart placeholder</span>
        </div>
        <div class="mt-4 grid grid-cols-2 gap-4">
          <div>
            <span class="text-sm text-gray-600">Monthly Revenue</span>
            <div class="text-xl font-semibold">$45,280</div>
          </div>
          <div>
            <span class="text-sm text-gray-600">Monthly Expenses</span>
            <div class="text-xl font-semibold">$12,450</div>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">Occupancy Rate</h3>
        <div class="h-48 flex items-center justify-center bg-gray-50 rounded">
          <span class="text-gray-400">Chart placeholder</span>
        </div>
        <div class="mt-4 grid grid-cols-2 gap-4">
          <div>
            <span class="text-sm text-gray-600">Current Rate</span>
            <div class="text-xl font-semibold">92%</div>
          </div>
          <div>
            <span class="text-sm text-gray-600">Target Rate</span>
            <div class="text-xl font-semibold">95%</div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class PerformanceChartsComponent {}