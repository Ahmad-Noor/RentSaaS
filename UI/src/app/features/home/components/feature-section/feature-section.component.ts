import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-feature-section',
  standalone: true,
  imports: [CommonModule],
  template: `
    <section class="bg-[#F8FBFF] py-16">
      <div class="max-w-7xl mx-auto px-4">
        <div class="flex items-center gap-12">
          <div class="flex-1">
            <h2 class="text-3xl font-bold mb-4">{{ title }}</h2>
            <p class="text-gray-600 mb-4">{{ description }}</p>
          </div>
          <div class="flex-1">
            <img [src]="imageUrl" [alt]="title" class="rounded-lg shadow-lg w-full">
          </div>
        </div>
      </div>
    </section>
  `
})
export class FeatureSectionComponent {
  @Input() title = '';
  @Input() description = '';
  @Input() imageUrl = '';
}