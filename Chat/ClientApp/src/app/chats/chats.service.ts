import { Inject, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { ChatModel } from "../models/ChatModel";
import { ChatModelList } from "../models/ChatModelList";

@Injectable({
    providedIn: 'root',
  })
export class ChatService {
    private baseUrl: string;

    constructor(private _client: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }
    getChats() : Observable<ChatModelList>{
        return this._client.get<ChatModelList>(this.baseUrl + 'api/chat/get-chats');
    }
}