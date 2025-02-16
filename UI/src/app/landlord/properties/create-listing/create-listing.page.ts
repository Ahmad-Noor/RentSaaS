import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router'; 
import { ListingFormComponent } from '../listing-form/listing-form.component';

@Component({
  selector: 'app-create-listing-page',
  standalone: true,
  imports: [CommonModule, RouterLink, ListingFormComponent],
  templateUrl:"create-listing.page.html"
})
export class CreateListingPage {
  handleSubmit(data: any): void {
    console.log('Listing data:', data);
    // TODO: Save listing and redirect back to listings page
  }
}