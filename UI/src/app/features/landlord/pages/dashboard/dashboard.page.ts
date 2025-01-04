import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ResourceOverviewComponent } from '../../components/dashboard/resource-overview/resource-overview.component';
import { PerformanceChartsComponent } from '../../components/dashboard/performance-charts/performance-charts.component';
import { MetricsGridComponent } from '../../components/dashboard/metrics-grid/metrics-grid.component';

@Component({
    selector: 'app-dashboard-page',
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