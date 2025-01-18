import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FeatureTab } from '../feature-tab.interface';

@Component({
    selector: 'app-tab-content',
    imports: [CommonModule],
    template: `
    <div class="flex items-center gap-12">
      <div class="flex-1">
        <h2 class="text-3xl font-bold mb-4">{{ content.title }}</h2>
        <p class="text-gray-600">{{ content.description }}</p>
      </div>
      <div class="flex-1">
        <img 
          [src]="content.imageUrl" 
          [alt]="content.title"
          class="rounded-lg shadow-lg w-full"
        >
      </div>
    </div>
  `
})
export class TabContentComponent {
  @Input() content!: FeatureTab['content'];
}