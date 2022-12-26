import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, convertToParamMap, Params, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { AuthService } from './auth.service';

@Injectable({
    providedIn: 'root',
})
export class AuthGuard implements CanActivate {
    constructor(
        private _authService: AuthService,
        private _router: Router,
    ) {}

    canActivate(_next: ActivatedRouteSnapshot, _state: RouterStateSnapshot): Observable<boolean | UrlTree>{

        return this._authService.isAuthenticated()
            .pipe(
                map(
                    (isLoggedIn: boolean) => {
                        if (!isLoggedIn) {
                            this._authService.login();
                            return false;
                        }

                        return true;
                    },
                ),
            )
        ;
    }
}
