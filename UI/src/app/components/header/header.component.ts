import { Component, Inject, OnInit, PLATFORM_ID } from "@angular/core";
import { RouterLink } from "@angular/router";
import { NavLinkComponent } from "../../shared/components/nav-link/nav-link.component";
import { ButtonComponent } from "../../shared/components/button/button.component";
import { MobileMenuComponent } from "../../shared/components/mobile-menu/mobile-menu.component";
import { AuthService } from "../../features/auth/services/auth.service";
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

  islogin: any;

  constructor(private _auth: AuthService, @Inject(PLATFORM_ID) private platformId: object) {
    //console.log("header is working ");

    if (isPlatformBrowser(platformId)) {


      //console.log("header is working ",localStorage.getItem("token"));
      if (localStorage.getItem("token")!=null) {
        this._auth.SaveData();  
    }


  }
}

  ngOnInit() {
this._auth.userData.subscribe((resulte)=>{
  if(resulte)
  {
    this.islogin=true;
  }
  else
  {
    this.islogin=false;
  }
})
  }


logout()
{
  this._auth.SignOut();
  this._auth.userData.next(null)
}

}
