import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-hero-cta',
  standalone: true,
  imports: [RouterLink],
  template: `
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
  `
})
export class HeroCtaComponent {}