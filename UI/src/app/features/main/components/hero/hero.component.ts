import { Component } from '@angular/core';
import { HeroContentComponent } from '../hero-content/hero-content.component';
import { HeroCtaComponent } from '../hero-cta/hero-cta.component';

@Component({
  selector: 'app-hero',
  standalone: true,
  imports: [HeroContentComponent, HeroCtaComponent],
  template: `
    <section class="bg-[#4B8FD9] text-white py-20 px-8">
      <div class="max-w-4xl mx-auto">
        <app-hero-content />
        <app-hero-cta />
      </div>
    </section>
  `
})
export class HeroComponent {}