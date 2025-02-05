import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
    selector: 'app-hero-section',
    imports: [CommonModule, RouterLink],
    template: `
    <section class="bg-[#F8FBFF] py-12 sm:py-20">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
        <h1 class="text-3xl sm:text-4xl md:text-5xl font-bold text-[#1B3A5C] mb-6">
          All the tools you need to keep your process in one place
        </h1>
        <p class="text-lg sm:text-xl text-gray-600 mb-8 max-w-3xl mx-auto">
          Work smarter, not harder. Consolidate all of your apps and paperwork in your filing cabinet.
          If you need even more from our rental management property software, upgrade to our Premium Plan.
        </p>
        <div class="flex flex-col sm:flex-row justify-center gap-4">
          <a 
            routerLink="/register"
            class="w-full sm:w-auto bg-orange-600 text-white px-8 py-3 rounded-md hover:bg-orange-700 transition-colors"
          >
            Get Started
          </a>
          <a 
            routerLink="/pricing"
            class="w-full sm:w-auto border border-orange-600 text-orange-600 px-8 py-3 rounded-md hover:bg-orange-50 transition-colors"
          >
            View Pricing
          </a>
        </div>
      </div>
    </section>
  `
})
export class HeroSectionComponent {}