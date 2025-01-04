import { Component, Input, Output, EventEmitter } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { SidebarItemComponent } from './sidebar-item.component';
import { SidebarGroupComponent } from './sidebar-group.component';
import { LANDLORD_NAVIGATION } from '../../../../../shared/constants/navigation';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, SidebarItemComponent, SidebarGroupComponent],
  template: `
    <aside 
      class="bg-[#213D5B] text-white h-screen fixed left-0 top-0 overflow-y-auto transition-all duration-300"
      [class.w-64]="!isCollapsed"
      [class.w-16]="isCollapsed"
    >
      <div class="p-4 border-b border-[#2B4D6F] flex items-center justify-center">
        @if (!isCollapsed) {
          <h1 class="text-xl font-semibold truncate">Property Manager</h1>
        } @else {
          <span class="material-icons text-2xl">home</span>
        }
      </div>
      
      @if (!isCollapsed) {
        <div class="p-4">
          <button class="w-full bg-[#107C10] text-white p-2 rounded mb-4 flex items-center justify-center gap-2 hover:bg-[#0B590B] transition-colors">
            <span class="material-icons text-sm">add</span>
            <span class="truncate">New Property</span>
          </button>
        </div>
      }

      <nav class="space-y-4">
        @for (group of navigation; track group.label) {
          <app-sidebar-group 
            [label]="group.label"
            [isCollapsed]="isCollapsed"
          >
            @for (item of group.items; track item.label) {
              <app-sidebar-item 
                [icon]="item.icon"
                [label]="item.label"
                [route]="item.route"
                [children]="item.children"
                [isCollapsed]="isCollapsed"
              />
            }
          </app-sidebar-group>
        }
      </nav>
    </aside>
  `
})
export class SidebarComponent {
  @Input() isCollapsed = false;
  @Output() isCollapsedChange = new EventEmitter<boolean>();
  
  navigation = LANDLORD_NAVIGATION;
}