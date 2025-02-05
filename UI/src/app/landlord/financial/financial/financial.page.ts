import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-financial-page',
  standalone: true,
  imports: [RouterLink],
  template: `
    <div class="space-y-6">
      <div class="flex justify-between items-center">
        <div>
          <h1 class="text-2xl font-semibold">Financial Overview</h1>
          <p class="mt-1 text-gray-600">Manage your property finances</p>
        </div>
      </div>

      <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
        <a 
          routerLink="expenses"
          class="bg-white p-6 rounded-lg shadow hover:shadow-md transition-shadow"
        >
          <div class="flex items-center gap-4">
            <span class="material-icons text-[#0078D4]">account_balance_wallet</span>
            <div>
              <h2 class="text-lg font-semibold">Expenses</h2>
              <p class="text-sm text-gray-600">Track and manage expenses</p>
            </div>
          </div>
        </a>

        <div class="bg-white p-6 rounded-lg shadow">
          <div class="flex items-center gap-4">
            <span class="material-icons text-[#0078D4]">payments</span>
            <div>
              <h2 class="text-lg font-semibold">Payments</h2>
              <p class="text-sm text-gray-600">Manage rent and other payments</p>
            </div>
          </div>
        </div>

        <div class="bg-white p-6 rounded-lg shadow">
          <div class="flex items-center gap-4">
            <span class="material-icons text-[#0078D4]">assessment</span>
            <div>
              <h2 class="text-lg font-semibold">Reports</h2>
              <p class="text-sm text-gray-600">Financial reports and analytics</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class FinancialPage {}