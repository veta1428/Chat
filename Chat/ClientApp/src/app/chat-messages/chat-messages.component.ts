import { Component, OnInit } from '@angular/core';
import { ChatMessagesService } from './chat-messages.service';

@Component({
  selector: 'app-chat-messages',
  templateUrl: './chat-messages.component.html',
  styleUrls: ['./chat-messages.component.scss']
})
export class ChatMessagesComponent implements OnInit
{
    constructor(private _chatMessagesService: ChatMessagesService) { }
    ngOnInit(): void {
        // ToDo?
    }
}
