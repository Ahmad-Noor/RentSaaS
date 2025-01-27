import { Component, Input, Output, EventEmitter } from '@angular/core';
import { NavbarActionsComponent } from './navbar-actions/navbar-actions.component';
import { NavbarSearchComponent } from './navbar-search/navbar-search.component';
import { AZURE_COLORS } from '../../../../../shared/constants/colors';
@Component({
  selector: "app-navbar",
  standalone: true,
  imports: [ NavbarSearchComponent],
  templateUrl: './navbar.component.html',
  styleUrl  :'./navbar.component.css'
})
export class NavbarComponent {
  @Input() sidebarCollapsed = false;
  @Output() toggleSidebar = new EventEmitter<void>();



  /**
   *
   */
  constructor() {
    const colors = AZURE_COLORS;    
  }
}