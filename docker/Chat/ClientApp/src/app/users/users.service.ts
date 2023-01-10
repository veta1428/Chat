import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { UserModel } from "../models/UserModel";

@Injectable({
    providedIn: 'root',
  })
export class UserService {
    constructor(private _client: HttpClient) {}
    getUsers() : Observable<UserModel[]>{
        return this._client.get<UserModel[]>('api/user/get-users');
    }
}