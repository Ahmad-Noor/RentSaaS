import { Component, Input, Output, EventEmitter, Inject, PLATFORM_ID } from '@angular/core';
import { AZURE_COLORS } from '../../../shared/constants/colors';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { initFlowbite } from 'flowbite';
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

 
  constructor(@Inject(PLATFORM_ID) private platformId: any) {
    const colors = AZURE_COLORS;    
  }
  ngOnInit(): void {
    if (isPlatformBrowser(this.platformId)) {
      initFlowbite();
    }
  }



}