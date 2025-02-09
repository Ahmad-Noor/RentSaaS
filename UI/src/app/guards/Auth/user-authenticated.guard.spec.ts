import { TestBed } from '@angular/core/testing';
import { CanActivateFn } from '@angular/router';

import { UserAuthenticatedGuard } from './user-authenticated.guard';

describe('UserAuthenticatedGuard', () => {
  const executeGuard: CanActivateFn = (...guardParameters) => 
      TestBed.runInInjectionContext(() => UserAuthenticatedGuard(...guardParameters));

  beforeEach(() => {
    TestBed.configureTestingModule({});
  });

  it('should be created', () => {
    expect(executeGuard).toBeTruthy();
  });
});
