import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportCategory } from '../../../../models/reports.types';

@Component({
  selector: 'app-report-card',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="bg-white rounded-lg shadow-sm">
      <div class="p-4 border-b">
        <div class="flex items-center gap-3">
          <span class="material-icons text-blue-600">{{ category.icon }}</span>
          <h2 class="text-lg font-semibold">{{ category.name }}</h2>
        </div>
      </div>
      
      <div class="p-4">
        <div class="space-y-3">
          @for (report of category.reports; track report.id) {
            <div class="flex items-center justify-between py-2">
              <div class="flex items-center gap-3">
                <span class="material-icons text-gray-400">description</span>
                <div>
                  <div>{{ report.name }}</div>
                  <div class="text-sm text-gray-500">{{ report.description }}</div>
                </div>
              </div>
              <div class="flex items-center gap-2">
                <button 
                  class="p-2 text-gray-600 hover:text-blue-600 rounded-lg hover:bg-blue-50"
                  (click)="generateReport(report)"
                >
                  <span class="material-icons">play_arrow</span>
                </button>
                <button 
                  class="p-2 text-gray-600 hover:text-blue-600 rounded-lg hover:bg-blue-50"
                  (click)="scheduleReport(report)"
                >
                  <span class="material-icons">schedule</span>
                </button>
              </div>
            </div>
          }
        </div>
      </div>
    </div>
  `
})
export class ReportCardComponent {
  @Input() category!: ReportCategory;

  generateReport(report: any): void {
    console.log('Generate report:', report);
  }

  scheduleReport(report: any): void {
    console.log('Schedule report:', report);
  }
}