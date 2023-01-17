import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MessageModel } from '../models/MessageModel';
import { Subscription } from 'rxjs';
import { ChatMessagesService } from './chat-messages.service';

@Component({
	selector: 'app-chat-messages',
	templateUrl: './chat-messages.component.html',
	styleUrls: ['./chat-messages.component.scss']
})

export class ChatMessagesComponent implements OnInit {

	public dataSource: MessageModel[] = [];
	public displayedColumns: string[] = ['firstName', 'lastName', 'sentDate', 'text'];
	public isLoading: boolean = true;

	private id: number = 0;
	private subsciption: Subscription;
	constructor(private _chatMessagesService: ChatMessagesService, private activateRoute: ActivatedRoute, private _detector: ChangeDetectorRef, private router: Router) {
		//get chatId from route
		this.subsciption = activateRoute.params.subscribe(params => this.id = params['chatId']);
	}

	ngOnInit(): void {
		this._chatMessagesService.getMessages(this.id).subscribe((data) => {
			this.dataSource = data.messageModels;
			this.isLoading = false;
			this._detector.detectChanges();
		})
	}

	ngAfterViewInit() {
		setInterval(() => {
			this._chatMessagesService.getMessages(this.id).subscribe((data) => {
				this.dataSource = data.messageModels;
				this.isLoading = false;
				this._detector.detectChanges();
			});
		}, 5000);
	}

	ngAfterViewChecked(): void{
		this.scrollMessagesToBottom();
	}

	onSendMessageClicked(): void {
		if ((<HTMLInputElement>document.getElementById("new-message-text")).value === "") {
			return;
		}

		this._chatMessagesService.sendMessage(this.id, (<HTMLInputElement>document.getElementById("new-message-text")).value).subscribe();

		this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
			this.router.navigate(['/chat-messages/' + this.id]));
	}

	formatDate(dateStr: String): String {
		let options: Intl.DateTimeFormatOptions = {
			year: 'numeric', month: 'numeric', day: 'numeric',
			hour: 'numeric', minute: 'numeric', second: 'numeric',
			hour12: false
		};

		var date = Date.parse(dateStr.toString());
		var formattedDate = new Intl.DateTimeFormat("ru-RU", options).format(date);
		return formattedDate;
	}

	scrollMessagesToBottom(): void {
		  let messagesTable = document.getElementById("messages-table");
		  messagesTable?.scroll ({
			top: messagesTable.scrollHeight,
			left: 0,
			behavior: 'auto'
		  });
	}
}
