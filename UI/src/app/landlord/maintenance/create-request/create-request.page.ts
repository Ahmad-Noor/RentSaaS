import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router, ActivatedRoute } from '@angular/router'; 
import { MaintenanceRequest } from '../types/maintenance.types';
import { MaintenanceService } from '../services/maintenance.service';
import { RequestFormComponent } from '../request-form/request-form.component';

@Component({
  selector: 'app-create-request-page',
  standalone: true,
  imports: [CommonModule, RouterLink, RequestFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Create Maintenance Request</h1>
          <p class="mt-1 text-gray-600">Submit a new maintenance request</p>
        </div>
        <a 
          routerLink=".."
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Requests
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-request-form (submit)="handleSubmit($event)" />
        </div>
      </div>
    </div>
  `
})
export class CreateRequestPage {
  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private maintenanceService: MaintenanceService
  ) {}

  handleSubmit(request: MaintenanceRequest): void {
    this.maintenanceService.createRequest(request);
    this.router.navigate(['..'], { relativeTo: this.route });
  }
}