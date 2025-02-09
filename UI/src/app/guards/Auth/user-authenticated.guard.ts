import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Constant } from '../../constants';

export const UserAuthenticatedGuard: CanActivateFn = (route, state) => {
  let routing = inject(Router);
  let platformid=inject(PLATFORM_ID) ; 

  if (isPlatformBrowser(platformid)) {
    const token = localStorage.getItem(Constant.token);
    const orgId = localStorage.getItem(Constant.OrganizationIdRentSass);
  
    if (!token && !orgId) {
      return true;
    }
  }
  
  routing.navigate(["/dashboard"]);
  return false;



  
};
