import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { LeaseListComponent } from '../../components/lease/lease-list/lease-list.component';

@Component({
  selector: 'app-lease-page',
  standalone: true,
  imports: [CommonModule, RouterLink, LeaseListComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Lease Agreements</h1>
          <p class="mt-1 text-gray-600">Manage and track lease agreements</p>
        </div>
        <a 
          routerLink="create"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">add</span>
          Create Agreement
        </a>
      </div>

      <app-lease-list />
    </div>
  `
})
export class LeasePage {}