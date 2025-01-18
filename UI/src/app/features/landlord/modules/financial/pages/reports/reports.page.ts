import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportGridComponent } from '../../components/reports/report-grid/report-grid.component';

@Component({
  selector: 'app-reports-page',
  standalone: true,
  imports: [CommonModule, ReportGridComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Financial Reports</h1>
          <p class="mt-1 text-gray-600">Generate and view financial reports for your properties</p>
        </div>
        <button class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors">
          <span class="material-icons text-sm">download</span>
          Export Reports
        </button>
      </div>

      <app-report-grid />
    </div>
  `
})
export class ReportsPage {}