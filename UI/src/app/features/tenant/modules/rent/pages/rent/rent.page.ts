import { Component } from '@angular/core';

@Component({
  selector: 'app-rent-page',
  standalone: true,
  template: `
    <div class="p-4">
      <h1 class="text-2xl font-bold mb-4">Pay Rent</h1>
      <div class="grid grid-cols-1 md:grid-cols-2 gap-4">
        <div class="bg-white rounded-lg shadow p-4">
          <h2 class="text-lg font-semibold mb-4">Payment Details</h2>
          <!-- Payment form -->
        </div>
        <div class="bg-white rounded-lg shadow p-4">
          <h2 class="text-lg font-semibold mb-4">Payment History</h2>
          <!-- Payment history -->
        </div>
      </div>
    </div>
  `
})
export class RentPage {}