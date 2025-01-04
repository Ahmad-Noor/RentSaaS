import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavItem } from '../../../../../shared/constants/navigation';

@Component({
  selector: 'app-sidebar-item',
  standalone: true,
  imports: [CommonModule, RouterLink, RouterLinkActive],
  template: `
    <div>
      <a 
        [routerLink]="route"
        routerLinkActive="bg-[#0078D4]"
        class="flex items-center gap-3 px-4 py-2 text-sm hover:bg-[#1B3A5C] transition-colors"
        [title]="isCollapsed ? label : ''"
      >
        <span class="material-icons text-xl min-w-[24px]">{{ icon }}</span>
        @if (!isCollapsed) {
          <span class="truncate">{{ label }}</span>
          @if (children?.length) {
            <span class="material-icons text-sm ml-auto">expand_more</span>
          }
        }
      </a>

      @if (!isCollapsed && children?.length) {
        <div class="ml-8 mt-1 space-y-1">
          @for (child of children; track child.label) {
            <a 
              [routerLink]="child.route"
              routerLinkActive="text-blue-400"
              class="flex items-center gap-3 px-4 py-1 text-sm text-gray-300 hover:text-white transition-colors"
            >
              <span class="material-icons text-sm">{{ child.icon }}</span>
              <span class="truncate">{{ child.label }}</span>
            </a>
          }
        </div>
      }
    </div>
  `
})
export class SidebarItemComponent {
  @Input() icon!: string;
  @Input() label!: string;
  @Input() route!: string;
  @Input() children?: NavItem[];
  @Input() isCollapsed = false;
}