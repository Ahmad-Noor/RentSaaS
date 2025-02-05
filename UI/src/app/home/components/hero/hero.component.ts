import { Component } from '@angular/core';
import { HeroContentComponent } from '../hero-content/hero-content.component';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [HeroContentComponent],
  template: `
    <section class="bg-[#4B8FD9] text-white py-20 px-8">
      <div class="max-w-4xl mx-auto">
        <app-hero-content />
    <div class="flex gap-4">
      <a 
        routerLink="/demo"
        class="bg-orange-500 text-white px-6 py-3 rounded-md hover:bg-orange-600 transition-colors"
      >
        See For Yourself
      </a>
      <a 
        routerLink="/pricing"
        class="bg-transparent border-2 border-white text-white px-6 py-3 rounded-md hover:bg-white/10 transition-colors"
      >
        View Pricing
      </a>
    </div>     
   </div>
    </section>
  `
})
export class HeroComponent {}