import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ChatModel } from '../models/ChatModel';
import { ChatService } from './chats.service';

@Component({
  selector: 'app-chats',
  templateUrl: './chats.component.html',
  styleUrls: ['./chats.component.scss']
})
export class ChatComponent implements OnInit{
    /**
     *
     */
    public dataSource: ChatModel[] = [];
    public displayedColumns: string[] = ["title", "createdDate"];
    public isLoading: boolean = true;
    constructor(private _chatSrevice: ChatService, private _detector: ChangeDetectorRef) {
        
    }

    ngOnInit(): void {
        this._chatSrevice.getChats().subscribe((data) =>{
            this.dataSource = data.chatList;
            this.isLoading = false;
            this._detector.detectChanges();
        })
    } 

    onRowClicked(chat: ChatModel): void{
        // ToDo: handle
    }
}
