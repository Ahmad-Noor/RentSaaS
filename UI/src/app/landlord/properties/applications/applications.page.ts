import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Application } from '../../../models/application.types'; 
import { SearchBarComponent } from '../../../shared/components/search-bar/search-bar.component'; 
import { ApplicationService } from '../../../service/application.service';

@Component({
  selector: 'app-applications-page',
  standalone: true,
  imports: [CommonModule, RouterLink, SearchBarComponent],
  templateUrl:"application.page.html"
})
export class ApplicationsPage {
  @Input() applications: Application[] = [];
  @Output() onAction = new EventEmitter<{ type: string; application: Application }>();
  filteredApplications: Application[] = [];

  constructor(private applicationService: ApplicationService) {
    // No need to declare applications here since it's passed through @Input()
    this.applicationService.getAllApplications().subscribe(applications => {
      console.log(applications)
      this.applications = applications.data;
      this.filteredApplications = applications.data;
    });
  }



  handleSearch(term: string): void {
    this.filteredApplications = this.applications.filter(app => 
      app.applicantEmail.toLowerCase().includes(term.toLowerCase()) ||
      app.phoneNumber 
     );
  }
  
  // handleAction(action: { type: string; application: Application }): void {
  //   switch (action.type) {
  //     case 'approve':
  //       this.applicationService.updateApplicationStatus(action.application.id, 'approved');
  //       break;
  //     case 'reject':
  //       this.applicationService.updateApplicationStatus(action.application.id, 'rejected');
  //       break;
  //     case 'delete':
  //       this.applicationService.deleteApplication(action.application.id);
  //       break;
  //   }
  // }

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
