import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { UserModel } from "../models/UserModel";

@Injectable({
    providedIn: 'root',
})
export class NewChatService {
    constructor(private _client: HttpClient) {   }

    getUsers(): Observable<UserModel[]> {
        return this._client.get<UserModel[]>('api/user/get-users');
    }

    createChat(chatUsers: UserModel[]): Observable<Object> {
        const body = {userIds: chatUsers.map(user => user.userId)};
        return this._client.post('api/chat/create-chat', body);
    }
}