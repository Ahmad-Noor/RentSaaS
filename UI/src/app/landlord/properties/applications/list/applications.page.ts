import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Application } from '../../../../models/application.types';
import { SearchBarComponent } from '../../../../shared/components/search-bar/search-bar.component';
import { ApplicationService } from '../../../../service/application.service';
import { ConfirmDialogService } from '../../../../shared/services/confirm-dialog.service';
import { RouterLink, Router, ActivatedRoute } from "@angular/router";
import { application } from 'express';


@Component({
  selector: 'app-applications-page',
  standalone: true,
  imports: [CommonModule, RouterLink, SearchBarComponent],
  templateUrl: "application.page.html"
})
export class ApplicationsPage implements OnInit {


  @Input() applications: Application[] = [];
  @Output() onAction = new EventEmitter<{ type: string; application: Application }>();
  filteredApplications: Application[] = [];


  applicationData!: Application[];

  constructor(private applicationService: ApplicationService, private confirmDialog: ConfirmDialogService, private router: Router,
    private route: ActivatedRoute,) {
    this.applicationService.getApplications().subscribe(applications => {
      console.log(applications); // تأكد من شكل البيانات في الكونسول
      this.applicationData = applications;
      this.applications = applications;
      this.filteredApplications = applications;
    });

  }

  ngOnInit(): void {
    this.loadApplicationss();
  }

  loadApplicationss() {
    this.applicationService.getApplications().subscribe({
      next: (applications) => {
        this.applications = this.processApplications(applications);
        this.filteredApplications = [...this.applications];
      },
      error: (error) => {
        console.error('Failed to load payments:', error);
      }
    });
  }

  processApplications(applications: any[]): Application[] {
    return applications.map(application => {
      // Derive status from isPaid and dueDate


      return {
        ...application,
        status: status
      };
    });
  }




  handleSearch(term: string): void {
    this.filteredApplications = this.applicationData.filter(app =>
      app.applicantEmail.toLowerCase().includes(term.toLowerCase()) ||
      app.phoneNumber
    );
  }

  handleEditAction(application: Application) {
    this.router.navigate(['application'], {
      relativeTo: this.route,
      state: { application }
    });
  }


  async handleDeleteAction(application: Application) {
    const isConfirmed = await this.confirmDialog.show({
      title: "Delete application?",
      message: "Are you sure you want to delete this application?",
      confirmText: "Delete",
      cancelText: "Cancel",
      type: "danger",
    });

    if (isConfirmed) {
      this.applicationService.deleteApplication(application.id).subscribe({
        next: () => {
          // Remove payment from both arrays to update UI
          this.applications = this.applications.filter(e => e.id !== application.id);
          this.filteredApplications = this.filteredApplications.filter(e => e.id !== application.id);
        },
        error: (error) => {
          console.error('Error deleting application:', error);
        }
      });
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
