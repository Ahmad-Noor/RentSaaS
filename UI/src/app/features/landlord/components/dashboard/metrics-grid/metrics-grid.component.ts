import { Component } from '@angular/core';

@Component({
  selector: 'app-metrics-grid',
  standalone: true,
  template: `
    <div class="grid grid-cols-3 gap-4">
      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">Financial Overview</h3>
        <div class="space-y-4">
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Total Revenue</span>
            <span class="text-lg font-semibold">$452,800</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Outstanding Balance</span>
            <span class="text-lg font-semibold">$12,450</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Late Payments</span>
            <span class="text-lg font-semibold">3</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">Maintenance Stats</h3>
        <div class="space-y-4">
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Open Requests</span>
            <span class="text-lg font-semibold">5</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Avg Response Time</span>
            <span class="text-lg font-semibold">1.2 days</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Completed This Month</span>
            <span class="text-lg font-semibold">28</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-4 rounded-lg shadow">
        <h3 class="text-lg font-semibold mb-4">Leasing Activity</h3>
        <div class="space-y-4">
          <div class="flex justify-between items-center">
            <span class="text-gray-600">New Applications</span>
            <span class="text-lg font-semibold">12</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Pending Renewals</span>
            <span class="text-lg font-semibold">8</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Move-outs Next Month</span>
            <span class="text-lg font-semibold">3</span>
          </div>
        </div>
      </div>
    </div>
  `
})
export class MetricsGridComponent {}