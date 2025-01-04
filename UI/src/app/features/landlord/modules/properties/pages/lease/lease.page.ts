import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-lease-page',
    imports: [CommonModule],
    template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <h1 class="text-2xl font-semibold">Lease Agreements</h1>
        <button class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors">
          <span class="material-icons text-sm">add</span>
          Create Agreement
        </button>
      </div>

      <div class="bg-white rounded-lg shadow p-6">
        <!-- Content will be added later -->
        <p>Lease agreement management content will go here</p>
      </div>
    </div>
  `
})
export class LeasePage {}