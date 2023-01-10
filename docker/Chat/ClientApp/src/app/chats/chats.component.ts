import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ChatModel } from '../models/ChatModel';
import { ChatService } from './chats.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-chats',
    templateUrl: './chats.component.html',
    styleUrls: ['./chats.component.scss']
})

export class ChatComponent implements OnInit {
    /**
     *
     */
    public dataSource: ChatModel[] = [];
    public displayedColumns: string[] = ["title", "createdDate"];
    public isLoading: boolean = true;
    constructor(private _chatSrevice: ChatService, private _detector: ChangeDetectorRef, private _router: Router) {

    }

    ngOnInit(): void {
        this._chatSrevice.getChats().subscribe((data) => {
            this.dataSource = data.chatList;
            this.isLoading = false;
            this._detector.detectChanges();
        })
    }

    onRowClicked(chat: ChatModel): void {
        this._router.navigate(['/chat-messages', chat.id]);
    }

    formatDate(dateStr: String): String {
        var date = Date.parse(dateStr.toString());
        //let options:  Intl.DateTimeFormatOptions = { year: 'numeric', month: 'numeric', day: 'numeric' };
        var formattedDate = new Intl.DateTimeFormat("ru-RU").format(date);
        return formattedDate;
    }

    onCreateChatBtnClicked(): void {
        this._router.navigate(['/new-chat']);
    }
}
