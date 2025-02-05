import { Component, Input, Output, EventEmitter } from '@angular/core';
import { AZURE_COLORS } from '../../../shared/constants/colors';
@Component({
  selector: "app-navbar",
  standalone: true,
  
  templateUrl: './navbar.component.html',
  styleUrl  :'./navbar.component.css'
})
export class NavbarComponent {
  @Input() sidebarCollapsed = false;
  @Output() toggleSidebar = new EventEmitter<void>();
  colors = AZURE_COLORS;

 
  constructor() {
    const colors = AZURE_COLORS;    
  }
}