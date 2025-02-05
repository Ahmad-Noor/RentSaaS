import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, Router } from '@angular/router'; 
import { ApplicationFormComponent } from '../application-form/application-form.component';

@Component({
  selector: 'app-send-application-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ApplicationFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Send Rental Application</h1>
          <p class="mt-1 text-gray-600">Send a rental application to potential tenants</p>
        </div>
        <a 
          routerLink=".."
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Applications
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-application-form (submit)="handleSubmit($event)" />
        </div>
      </div>
    </div>
  `
})
export class SendApplicationPage {
  constructor(private router: Router) {}

  handleSubmit(data: any): void {
    console.log('Application data:', data);
    // TODO: Implement application sending logic
    this.router.navigate(['/landlord/properties/applications']);
  }
}