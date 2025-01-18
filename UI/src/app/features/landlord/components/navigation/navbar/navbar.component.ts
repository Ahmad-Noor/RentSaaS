import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NavbarActionsComponent } from './navbar-actions.component';
import { NavbarSearchComponent } from './navbar-search.component';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [NavbarActionsComponent, NavbarSearchComponent],
  template: `
    <nav 
      class="h-14 bg-[#213D5B] text-white fixed top-0 right-0 z-10 transition-all duration-300"
      [class.left-64]="!sidebarCollapsed"
      [class.left-16]="sidebarCollapsed"
    >
      <div class="flex items-center justify-between h-full px-4">
        <div class="flex items-center gap-4">
          <button 
            class="p-1 hover:bg-[#0078D4] rounded transition-colors"
            (click)="toggleSidebar.emit()"
          >
            <span class="material-icons">{{ sidebarCollapsed ? 'menu_open' : 'menu' }}</span>
          </button>
          <h2 class="text-lg">Dashboard</h2>
        </div>
        
        <div class="flex items-center gap-4">
          <app-navbar-search />
          <app-navbar-actions />
        </div>
      </div>
    </nav>
  `
})
export class NavbarComponent {
  @Input() sidebarCollapsed = false;
  @Output() toggleSidebar = new EventEmitter<void>();
}