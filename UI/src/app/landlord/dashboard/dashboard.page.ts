import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResourceOverviewComponent } from './resource-overview.component';
import { PerformanceChartsComponent } from './performance-charts.component';
import { MetricsGridComponent } from './metrics-grid.component';
// import { ResourceOverviewComponent } from '../../dashboard/resource-overview/resource-overview.component';
// import { PerformanceChartsComponent } from '../../dashboard/performance-charts/performance-charts.component'; 

@Component({
  selector: 'app-dashboard-page',
  standalone: true,
  imports: [
    CommonModule,
    ResourceOverviewComponent,
    PerformanceChartsComponent,
    MetricsGridComponent
  ],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center mb-6">
        <h1 class="text-2xl font-semibold">Dashboard</h1>
      </div>
      
      <app-resource-overview />
      <app-performance-charts />
      <app-metrics-grid />
    </div>
  `
})
export class DashboardPage {}