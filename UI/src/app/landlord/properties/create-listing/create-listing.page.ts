import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router'; 
import { ListingFormComponent } from '../listing-form/listing-form.component';

@Component({
  selector: 'app-create-listing-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ListingFormComponent],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Create Property Listing</h1>
          <p class="mt-1 text-gray-600">Create and publish your property listing to multiple platforms</p>
        </div>
        <a 
          routerLink=".."
          class="text-gray-600 hover:text-gray-900 flex items-center gap-2"
        >
          <span class="material-icons">arrow_back</span>
          Back to Listings
        </a>
      </div>

      <div class="bg-white rounded-lg shadow">
        <div class="p-6">
          <app-listing-form (submit)="handleSubmit($event)" />
        </div>
      </div>
    </div>
  `
})
export class CreateListingPage {
  handleSubmit(data: any): void {
    console.log('Listing data:', data);
    // TODO: Save listing and redirect back to listings page
  }
}