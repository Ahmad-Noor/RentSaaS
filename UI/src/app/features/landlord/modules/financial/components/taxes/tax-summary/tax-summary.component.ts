import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tax-summary',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="grid grid-cols-1 md:grid-cols-3 gap-6">
      <div class="bg-white p-6 rounded-lg shadow-sm">
        <div class="flex items-center justify-between mb-4">
          <h3 class="text-lg font-semibold">Property Tax</h3>
          <span class="material-icons text-blue-600">home</span>
        </div>
        <div class="space-y-2">
          <div class="flex justify-between">
            <span class="text-gray-600">Total Due</span>
            <span class="font-semibold">$24,500</span>
          </div>
          <div class="flex justify-between">
            <span class="text-gray-600">Paid YTD</span>
            <span class="text-green-600">$12,250</span>
          </div>
          <div class="flex justify-between">
            <span class="text-gray-600">Next Due</span>
            <span>Mar 15, 2024</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-6 rounded-lg shadow-sm">
        <div class="flex items-center justify-between mb-4">
          <h3 class="text-lg font-semibold">Income Tax</h3>
          <span class="material-icons text-blue-600">account_balance</span>
        </div>
        <div class="space-y-2">
          <div class="flex justify-between">
            <span class="text-gray-600">Estimated Tax</span>
            <span class="font-semibold">$18,750</span>
          </div>
          <div class="flex justify-between">
            <span class="text-gray-600">Quarterly Paid</span>
            <span class="text-green-600">$4,687</span>
          </div>
          <div class="flex justify-between">
            <span class="text-gray-600">Next Payment</span>
            <span>Apr 15, 2024</span>
          </div>
        </div>
      </div>

      <div class="bg-white p-6 rounded-lg shadow-sm">
        <div class="flex items-center justify-between mb-4">
          <h3 class="text-lg font-semibold">Tax Deductions</h3>
          <span class="material-icons text-blue-600">savings</span>
        </div>
        <div class="space-y-2">
          <div class="flex justify-between">
            <span class="text-gray-600">Total Deductions</span>
            <span class="font-semibold">$8,250</span>
          </div>
          <div class="flex justify-between">
            <span class="text-gray-600">Categories</span>
            <span>6</span>
          </div>
          <div class="flex justify-between">
            <span class="text-gray-600">Last Updated</span>
            <span>2 days ago</span>
          </div>
        </div>
      </div>
    </div>
  `
})
export class TaxSummaryComponent {}