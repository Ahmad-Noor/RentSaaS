import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-applications-page',
    imports: [CommonModule],
    template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Applications & Leads</h1>
      </div>

      <div class="bg-white rounded-lg shadow p-6">
        <!-- Content will be added later -->
        <p>Applications and leads management content will go here</p>
      </div>
    </div>
  `
})
export class ApplicationsPage {}