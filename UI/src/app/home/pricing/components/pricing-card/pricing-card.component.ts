import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-pricing-card',
    imports: [CommonModule],
    template: `
    <div class="bg-white p-6 rounded-lg shadow-lg border relative" [class.border-orange-500]="featured">
      <div *ngIf="featured" class="absolute -top-3 right-4">
        <span class="bg-orange-500 text-white px-3 py-1 rounded-full text-sm">Popular</span>
      </div>
      
      <div class="text-center mb-6">
        <h3 class="text-lg font-semibold mb-2">{{name}}</h3>
        <div class="flex items-baseline justify-center">
          <span class="text-3xl font-bold">$</span>
          <span class="text-3xl font-bold">{{price}}</span>
          <span class="text-gray-500 ml-1">/mo</span>
        </div>
        <p *ngIf="description" class="text-sm text-gray-500 mt-2">{{description}}</p>
      </div>

      <ul class="space-y-3 mb-6">
        <li *ngFor="let feature of features" class="flex items-center">
          <span class="material-icons text-orange-500 mr-2">check_circle</span>
          <span>{{feature}}</span>
        </li>
      </ul>

      <button 
        class="w-full py-2 px-4 rounded-md text-center transition-colors"
        [class.bg-orange-600]="featured"
        [class.hover:bg-orange-700]="featured"
        [class.text-white]="featured"
        [class.bg-gray-100]="!featured"
        [class.hover:bg-gray-200]="!featured"
        [class.text-gray-800]="!featured"
      >
        Get Started
      </button>
    </div>
  `
})
export class PricingCardComponent {
  @Input() name = '';
  @Input() price = '0';
  @Input() description = '';
  @Input() features: string[] = [];
  @Input() featured = false;
}