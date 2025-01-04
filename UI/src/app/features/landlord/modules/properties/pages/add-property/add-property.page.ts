import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { PropertyFormComponent } from '../../components/property-form/property-form.component';

@Component({
  selector: 'app-add-property-page',
  standalone: true,
  imports: [CommonModule, RouterLink, PropertyFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">First, let's add your property</h1>
          <p class="mt-2 text-gray-600">
            Once you add your property, you can list it for free on Zillow, Trulia, and HotPads to help
            find your perfect renter.
          </p>
        </div>
        <a 
          routerLink=".."
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Properties
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-property-form (onSubmit)="handleSubmit($event)" />
        </div>
      </div>
    </div>
  `
})
export class AddPropertyPage {
  constructor(private router: Router) {}

  handleSubmit(data: any): void {
    console.log('Property data:', data);
    // TODO: Save property data
    this.router.navigate(['/landlord/properties']);
  }
}