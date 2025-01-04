import { Component } from '@angular/core';

@Component({
  selector: 'app-forms-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">Forms & Notices</h1>
      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Lease Agreements</h2>
          <!-- Lease forms content -->
        </div>
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Notice Templates</h2>
          <!-- Notice templates content -->
        </div>
        <div class="bg-white p-4 rounded-lg shadow">
          <h2 class="text-lg font-semibold mb-2">Custom Forms</h2>
          <!-- Custom forms content -->
        </div>
      </div>
    </div>
  `
})
export class FormsPage {}