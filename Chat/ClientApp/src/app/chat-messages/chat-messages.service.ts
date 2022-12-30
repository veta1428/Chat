import { Inject, Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { map, Observable } from "rxjs";
import { MessageModelList } from "../models/MessageModelList";

@Injectable({
	providedIn: 'root',
})
export class ChatMessagesService {
	private baseUrl: string;

	constructor(private _client: HttpClient, @Inject('BASE_URL') baseUrl: string) {
		this.baseUrl = baseUrl;
	}

	getMessages(id: number): Observable<MessageModelList> {
		return this._client.get<MessageModelList>(this.baseUrl + 'api/message/get-messages/' + id);
	}

	sendMessage(chatIdTo: number, text: String): Observable<Object> {
		const body = { chatIdTo: chatIdTo, text: text };
		return this._client.post('api/message/send-message', body);
	}
}