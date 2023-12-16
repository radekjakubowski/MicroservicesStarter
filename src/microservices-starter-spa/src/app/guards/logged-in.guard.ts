import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { SessionService } from '../services/session.service';

export const loggedInGuard: CanActivateFn = (route, state) => {
  var session = inject(SessionService);
  var router = inject(Router);

  if (session.isLoggedIn) {
    return true;
  }

  return router.navigateByUrl('/');
};
