import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NavLinkComponent } from '../../shared/components/nav-link/nav-link.component';
import { ButtonComponent } from '../../shared/components/button/button.component';
import { MobileMenuComponent } from '../../shared/components/mobile-menu/mobile-menu.component';

@Component({
    selector: 'app-header',
    imports: [RouterLink, NavLinkComponent, MobileMenuComponent],
    standalone :  true,
    templateUrl: './header.component.html',
    styleUrl:   './header.componen.css'
})
export class HeaderComponent {
  isMobileMenuOpen = false;

  toggleMobileMenu(): void {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  closeMobileMenu(): void {
    this.isMobileMenuOpen = false;
  }
}