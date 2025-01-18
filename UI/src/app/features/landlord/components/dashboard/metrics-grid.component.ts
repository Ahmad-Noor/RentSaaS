import { Component } from '@angular/core';
import { DashboardCardComponent } from './dashboard-card.component';

@Component({
  selector: 'app-metrics-grid',
  standalone: true,
  imports: [DashboardCardComponent],
  template: `
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
      <app-dashboard-card title="Resource Usage">
        <div class="space-y-4">
          <div class="flex justify-between items-center">
            <span class="text-gray-600">CPU Usage</span>
            <span class="text-lg font-semibold">45%</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Memory Usage</span>
            <span class="text-lg font-semibold">2.88 GB</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Storage Usage</span>
            <span class="text-lg font-semibold">1.75 GB</span>
          </div>
        </div>
      </app-dashboard-card>

      <app-dashboard-card title="Performance Metrics">
        <div class="space-y-4">
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Response Time</span>
            <span class="text-lg font-semibold">189.32 ms</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Requests/sec</span>
            <span class="text-lg font-semibold">5.2k</span>
          </div>
        </div>
      </app-dashboard-card>

      <app-dashboard-card title="System Health">
        <div class="space-y-4">
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Status</span>
            <span class="text-green-600 font-semibold">Healthy</span>
          </div>
          <div class="flex justify-between items-center">
            <span class="text-gray-600">Uptime</span>
            <span class="text-lg font-semibold">99.9%</span>
          </div>
        </div>
      </app-dashboard-card>
    </div>
  `
})
export class MetricsGridComponent {}