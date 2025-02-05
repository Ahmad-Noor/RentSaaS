import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Application } from '../../types/application.types';
import { ApplicationService } from '../../services/application.service';
import { SearchBarComponent } from '../../../../../shared/components/search-bar/search-bar.component';

@Component({
  selector: 'app-applications-page',
  standalone: true,
  imports: [CommonModule, RouterLink, SearchBarComponent],
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

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <div class="flex items-center justify-between mb-6">
            <app-search-bar 
              placeholder="Search applications"
              (onSearch)="handleSearch($event)"
            />
            <div class="flex gap-2">
              <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
                <span class="material-icons">filter_list</span>
              </button>
              <button class="p-2 text-gray-600 hover:bg-gray-100 rounded">
                <span class="material-icons">download</span>
              </button>
            </div>
          </div>
 
          <table class="w-full">
            <thead>
              <tr class="border-b">
                <th class="text-left py-3 px-4">Applicant</th>
                <th class="text-left py-3 px-4">Property</th>
                <th class="text-left py-3 px-4">Submitted</th>
                <th class="text-left py-3 px-4">Status</th>
                <th class="text-left py-3 px-4">Actions</th>
              </tr>
            </thead>
            <tbody>
              <tr *ngFor="let application of filteredApplications" class="border-b hover:bg-gray-50">
                <td class="py-3 px-4">
                  <div>{{ application.applicantName }}</div>
                  <div class="text-sm text-gray-500">{{ application.email }}</div>
                </td>
                <td class="py-3 px-4">{{ application.propertyName }}</td>
                <td class="py-3 px-4">
                  {{ application.submittedAt | date:'mediumDate' }}
                </td>
                <td class="py-3 px-4">
                  <span [class]="getStatusClass(application.status)">
                    {{ application.status }}
                  </span>
                </td>
                <td class="py-3 px-4">
                  <div class="flex gap-2">
                    <button *ngIf="application.status === 'new' || application.status === 'reviewing'" 
                      class="p-1 text-green-600 hover:bg-green-50 rounded"
                      (click)="onAction.emit({ type: 'approve', application })">
                      <span class="material-icons">check_circle</span>
                    </button>
                    <button *ngIf="application.status === 'new' || application.status === 'reviewing'" 
                      class="p-1 text-red-600 hover:bg-red-50 rounded"
                      (click)="onAction.emit({ type: 'reject', application })">
                      <span class="material-icons">cancel</span>
                    </button>
                    <button 
                      class="p-1 text-gray-600 hover:bg-gray-100 rounded"
                      (click)="onAction.emit({ type: 'delete', application })">
                      <span class="material-icons">delete</span>
                    </button>
                  </div>
                </td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `
})
export class ApplicationsPage {
  @Input() applications: Application[] = [];
  @Output() onAction = new EventEmitter<{ type: string; application: Application }>();
  filteredApplications: Application[] = [];

  constructor(private applicationService: ApplicationService) {
    // No need to declare applications here since it's passed through @Input()
    this.applicationService.getApplications().subscribe(applications => {
      this.applications = applications;
      this.filteredApplications = applications;
    });
  }

  handleSearch(term: string): void {
    this.filteredApplications = this.applications.filter(app => 
      app.applicantName.toLowerCase().includes(term.toLowerCase()) ||
      app.propertyName.toLowerCase().includes(term.toLowerCase()) ||
      app.email.toLowerCase().includes(term.toLowerCase())
    );
  }

  handleAction(action: { type: string; application: Application }): void {
    switch (action.type) {
      case 'approve':
        this.applicationService.updateApplicationStatus(action.application.id, 'approved');
        break;
      case 'reject':
        this.applicationService.updateApplicationStatus(action.application.id, 'rejected');
        break;
      case 'delete':
        this.applicationService.deleteApplication(action.application.id);
        break;
    }
  }

  getStatusClass(status: string): string {
    const baseClasses = 'px-2 py-1 rounded-full text-sm capitalize';
    const statusClasses: Record<string, string> = {
      'new': 'bg-blue-100 text-blue-800',
      'reviewing': 'bg-yellow-100 text-yellow-800',
      'approved': 'bg-green-100 text-green-800',
      'rejected': 'bg-red-100 text-red-800'
    };

    return `${baseClasses} ${statusClasses[status] || 'bg-gray-100 text-gray-800'}`;
  }
}
