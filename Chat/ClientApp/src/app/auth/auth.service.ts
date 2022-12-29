import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, map, Observable, of } from 'rxjs';
import { UserModel } from '../models/UserModel';

@Injectable({
    providedIn: 'root',
})
export class AuthService 
{
    constructor(
        private _http: HttpClient,
    ) { }

    login() {
        window.location.href = '/membership/login';
    }

    logout(): void {
        window.location.href = '/membership/logout';
    }

    isAuthenticated() :  Observable<boolean>
    {
        return this._http.get<UserModel>("api/account/info").pipe(map(user => user !== null), catchError(() => of(false)));
    }

    getUserInfo() :  Observable<UserModel>
    {
        return this._http.get<UserModel>("api/account/info");
    }
}