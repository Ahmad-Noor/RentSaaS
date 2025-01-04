import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NavbarActionsComponent } from './navbar-actions.component';
import { NavbarSearchComponent } from './navbar-search.component';

@Component({
    selector: 'app-navbar',
    imports: [NavbarActionsComponent, NavbarSearchComponent],
    templateUrl: `./navbar.component.html`,
    standalone :  true,
    styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  @Input() sidebarCollapsed = false;
  @Output() toggleSidebar = new EventEmitter<void>();
}