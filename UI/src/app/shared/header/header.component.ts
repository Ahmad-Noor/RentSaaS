import { Component, Inject, OnInit, PLATFORM_ID } from "@angular/core";
import { RouterLink } from "@angular/router";
import { NavLinkComponent } from "../components/nav-link/nav-link.component";
import { MobileMenuComponent } from "../components/mobile-menu/mobile-menu.component";
import { AuthService } from "../../auth/services/auth.service";
import { isPlatformBrowser } from "@angular/common";

@Component({
  selector: "app-header",
  imports: [RouterLink, NavLinkComponent, MobileMenuComponent],
  standalone: true,
  templateUrl: "./header.component.html",
  styleUrl: "./header.component.css",
})
export class HeaderComponent implements OnInit {
  isMobileMenuOpen = false;

  toggleMobileMenu(): void {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  closeMobileMenu(): void {
    this.isMobileMenuOpen = false;
  }

  isLogin: any;

  constructor(
    private _auth: AuthService,
    @Inject(PLATFORM_ID) private platformId: object
  ) {
    if (isPlatformBrowser(platformId)) {
      if (localStorage.getItem("token") != null) {
        this._auth.SaveData();
      }
    }
  }

  ngOnInit() {
    this._auth.userData.subscribe((result) => {
      if (result) {
        this.isLogin = true;
      } else {
        this.isLogin = false;
      }
    });
  }

  logout() {
    this._auth.SignOut();
    this._auth.userData.next(null);
  }
}
