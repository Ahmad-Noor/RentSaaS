import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-dashboard-card',
  standalone: true,
  template: `
    <div class="bg-white rounded-lg shadow p-4">
      <div class="flex justify-between items-center mb-4">
        <h2 class="text-lg font-semibold">{{ title }}</h2>
        <button class="text-blue-600 hover:text-blue-700 text-sm">Edit</button>
      </div>
      <ng-content></ng-content>
    </div>
  `
})
export class DashboardCardComponent {
  @Input() title!: string;
}