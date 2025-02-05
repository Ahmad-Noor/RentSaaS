import { Component } from '@angular/core';
import { CommonModule } from '@angular/common'; 
import { FormsGridComponent } from './forms-grid/forms-grid.component';

@Component({
  selector: 'app-forms-page',
  standalone: true,
  imports: [CommonModule, FormsGridComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Forms & Notices</h1>
          <p class="mt-1 text-gray-600">Access and manage all your property management forms</p>
        </div>
        <button class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors">
          <span class="material-icons text-sm">upload_file</span>
          Upload Custom Form
        </button>
      </div>

      <app-forms-grid />
    </div>
  `
})
export class FormsPage {}