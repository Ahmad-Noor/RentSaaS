import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../../navigation/sidebar/sidebar.component';
import { NavbarComponent } from '../../navigation/navbar/navbar.component';

@Component({
  selector: 'app-landlord-portal',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, NavbarComponent],
  template: `
    <div class="min-h-screen bg-[#F0F2F5]">
      <app-sidebar 
        [isCollapsed]="sidebarCollapsed"
        (isCollapsedChange)="handleSidebarCollapse($event)" 
      />
      <app-navbar 
        [sidebarCollapsed]="sidebarCollapsed"
        (toggleSidebar)="toggleSidebar()" 
      />
      
      <main [class]="getMainClass()">
        <div class="p-6">
          <router-outlet></router-outlet>
        </div>
      </main>
    </div>
  `
})
export class LandlordPortalPage {
  sidebarCollapsed = false;

  getMainClass(): string {
    const baseClass = 'pt-14 transition-all duration-300';
    return this.sidebarCollapsed 
      ? `${baseClass} ml-16` 
      : `${baseClass} ml-64`;
  }

  handleSidebarCollapse(collapsed: boolean): void {
    this.sidebarCollapsed = collapsed;
  }

  toggleSidebar(): void {
    this.sidebarCollapsed = !this.sidebarCollapsed;
  }
}