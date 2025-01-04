import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar-group',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="px-4 py-2">
      @if (!isCollapsed) {
        <h2 class="text-sm font-medium text-gray-400 mb-2 truncate">{{ label }}</h2>
      }
      <div class="space-y-1">
        <ng-content></ng-content>
      </div>
    </div>
  `
})
export class SidebarGroupComponent {
  @Input() label!: string;
  @Input() isCollapsed = false;
}