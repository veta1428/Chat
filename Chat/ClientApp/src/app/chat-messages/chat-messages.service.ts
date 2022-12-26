import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';

@Injectable({
    providedIn: 'root',
  })
export class ChatMessagesService {
    constructor(private _client: HttpClient) {}
    // ToDo
}