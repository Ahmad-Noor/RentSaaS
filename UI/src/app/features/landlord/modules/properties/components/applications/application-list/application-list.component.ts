import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApplicationService } from '../../../services/application.service';
import { Application } from '../../../types/application.types';
import { ApplicationTableComponent } from '../application-table/application-table.component';
import { SearchBarComponent } from '../../../../../../../shared/components/search-bar/search-bar.component';

@Component({
  selector: 'app-application-list',
  standalone: true,
  imports: [CommonModule, ApplicationTableComponent, SearchBarComponent],
  template: `
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

        <app-application-table
          [applications]="filteredApplications"
          (onAction)="handleAction($event)"
        />
      </div>
    </div>
  `
})
export class ApplicationListComponent {
  applications: Application[] = [];
  filteredApplications: Application[] = [];

  constructor(private applicationService: ApplicationService) {
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
}