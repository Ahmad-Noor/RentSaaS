import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { Constant } from '../../constants';

export const authGuard: CanActivateFn = (route, state) => {

  let routing = inject(Router);
  let platformed=inject(PLATFORM_ID) ; 

  if (isPlatformBrowser(platformed)) {
    const token = localStorage.getItem(Constant.token);
    const orgId = localStorage.getItem(Constant.OrganizationIdRentSass);
  
    if (token && orgId) {
      return true;
    }
  }
  
  routing.navigate(["/login"]);
  return false;
  
};
