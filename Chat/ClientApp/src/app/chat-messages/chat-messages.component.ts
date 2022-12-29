import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { ChatMessagesService } from './chat-messages.service';

@Component({
  selector: 'app-chat-messages',
  templateUrl: './chat-messages.component.html',
  styleUrls: ['./chat-messages.component.scss']
})
export class ChatMessagesComponent implements OnInit
{
  //id: number | undefined;
  //private subsciption: Subscription;
    constructor( _chatMessagesService: ChatMessagesService, private activateRoute: ActivatedRoute) {
      //get chatId from route
      //this.subsciption = activateRoute.params.subscribe(params => this.id = params['chatId']);
     }
    ngOnInit(): void {
        // ToDo?
    }
}
