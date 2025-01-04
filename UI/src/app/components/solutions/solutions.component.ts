import { Component } from '@angular/core';

@Component({
  selector: 'app-solutions',
  standalone: true,
  template: `
    <section class="py-16 px-8">
      <h2 class="text-3xl text-center mb-12">
        AN ALL-IN-ONE SOLUTION FOR SIMPLER PROPERTY MANAGEMENT
      </h2>
      <div class="grid grid-cols-3 gap-8 max-w-6xl mx-auto">
        <div class="text-center p-6">
          <h3 class="text-xl font-bold mb-4">Accounting</h3>
          <p>Complete accounting system designed for property management</p>
        </div>
        <div class="text-center p-6">
          <h3 class="text-xl font-bold mb-4">Reporting</h3>
          <p>Comprehensive reporting tools for better insights</p>
        </div>
        <div class="text-center p-6">
          <h3 class="text-xl font-bold mb-4">Marketing & Leasing</h3>
          <p>Streamline your marketing and leasing processes</p>
        </div>
      </div>
    </section>
  `
})
export class SolutionsComponent {}