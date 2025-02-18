import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Constant } from '../../constants';

export const UserAuthenticatedGuard: CanActivateFn = (route, state) => {
  let routing = inject(Router);
  let platformed=inject(PLATFORM_ID) ; 

  if (isPlatformBrowser(platformed)) {
    const token = localStorage.getItem(Constant.token);
  
    if (!token) {
      return true;
    }
  }
  
  routing.navigate(["/dashboard"]);
  return false;



  
};
