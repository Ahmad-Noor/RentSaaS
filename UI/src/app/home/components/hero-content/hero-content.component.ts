import { Component } from '@angular/core';

@Component({
  selector: 'app-hero-content',
  standalone: true,
  template: `
    <h1 class="text-5xl font-bold mb-6">
      Property Management Software for Every Portfolio
    </h1>
    <p class="text-xl mb-8">
      Our software is a powerful property management solution that combines all the features 
      you need to run your business into a single integrated solution.
    </p>
  `
})
export class HeroContentComponent {}