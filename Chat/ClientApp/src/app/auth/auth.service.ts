import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class AuthService 
{
    constructor(
        private _http: HttpClient,
    ) { }

    login() {
        window.location.href = '/membership/login?returnUrl=/chats';
    }

    logout(): void {
        window.location.href = '/membership/logout';
    }

    isAuthenticated() :  boolean 
    {
        return false;
    }
}
