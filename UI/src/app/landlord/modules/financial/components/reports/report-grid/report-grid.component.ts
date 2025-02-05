import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReportCardComponent } from '../report-card/report-card.component';
import { REPORT_CATEGORIES } from '../../../data/reports.data';

@Component({
  selector: 'app-report-grid',
  standalone: true,
  imports: [CommonModule, ReportCardComponent],
  template: `
    <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
      @for (category of categories; track category.id) {
        <app-report-card [category]="category" />
      }
    </div>
  `
})
export class ReportGridComponent {
  categories = REPORT_CATEGORIES;
}