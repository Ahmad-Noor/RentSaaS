import { Component } from '@angular/core';

@Component({
  selector: 'app-integrations',
  standalone: true,
  template: `
    <section class="py-16 px-8 text-center">
      <h2 class="text-3xl mb-12">Capabilities Beyond the Software</h2>
      <div class="flex justify-center gap-12 mb-8">
        <div class="w-24 h-12 bg-gray-200 rounded"></div>
        <div class="w-24 h-12 bg-gray-200 rounded"></div>
        <div class="w-24 h-12 bg-gray-200 rounded"></div>
      </div>
      <button class="text-blue-600">View All Integrations →</button>
    </section>
  `
})
export class IntegrationsComponent {}