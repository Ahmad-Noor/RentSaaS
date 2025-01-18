import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { ApplicationListComponent } from '../../components/applications/application-list/application-list.component';

@Component({
  selector: 'app-applications-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ApplicationListComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Applications & Leads</h1>
          <p class="mt-1 text-gray-600">Manage and track rental applications</p>
        </div>
        <a 
          routerLink="send"
          class="bg-[#0078D4] text-white px-4 py-2 rounded flex items-center gap-2 hover:bg-[#106EBE] transition-colors"
        >
          <span class="material-icons text-sm">send</span>
          Send Application
        </a>
      </div>

      <app-application-list />
    </div>
  `
})
export class ApplicationsPage {}