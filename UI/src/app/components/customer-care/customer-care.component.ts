import { Component } from '@angular/core';

@Component({
  selector: 'app-customer-care',
  standalone: true,
  template: `
    <section class="bg-gray-50 py-16 px-8">
      <div class="max-w-6xl mx-auto flex gap-12 items-center">
        <div class="flex-1">
          <h2 class="text-3xl font-bold mb-6">Top-Notch Customer Care</h2>
          <p class="mb-6">
            Our success is not possible without your success. That's why our focus remains on 
            cultivating lasting business relationships with our customers.
          </p>
          <div class="flex gap-4">
            <button class="text-blue-600">Onboarding →</button>
            <button class="text-blue-600">Customer Support →</button>
            <button class="text-blue-600">Training →</button>
          </div>
        </div>
        <div class="flex-1">
          <div class="bg-gray-200 h-64 rounded-lg"></div>
        </div>
      </div>
    </section>
  `
})
export class CustomerCareComponent {}