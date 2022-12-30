import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { NewChatUserModel } from '../models/NewChatUserModel';
import { NewChatService } from './new-chat.service';
import { Router } from '@angular/router';
import { MatCheckbox, MatCheckboxChange, MatCheckboxClickAction } from '@angular/material/checkbox';

@Component({
    selector: 'app-new-chat',
    templateUrl: './new-chat.component.html',
    styleUrls: ['./new-chat.component.scss']
})
export class NewChatComponent implements OnInit {
    /**
     *
     */
    public dataSource: UserModel[] = [];
    public displayedColumns: string[] = ['checkboxClmn', 'firstName', 'lastName'];
    public userModelColumns: string[] = this.displayedColumns.slice(1, 3);
    public isLoading: boolean = true;

    private chatUsers: UserModel[] = [];

    constructor(private _newChatService: NewChatService, private _detector: ChangeDetectorRef, private router: Router) {

    }

    ngOnInit(): void {
        this._newChatService.getUsers().subscribe((data) => {
            this.dataSource = data;
            this.isLoading = false;
            this._detector.detectChanges();
        })
    }

    onCheckboxClicked(event: MatCheckboxChange, user: NewChatUserModel): void {
        var userModel: UserModel = {
            firstName: user.firstName,
            lastName: user.lastName,
            userId: user.userId,
        };
        if (event.checked) {
            this.chatUsers.push(userModel);
        } else {
            this.chatUsers.splice(this.chatUsers.indexOf(userModel), 1);
        }
    }

    onCreateChatClicked (): void{
        if (this.chatUsers.length == 0){
            this.setErrorMessage("Choose at least 1 chat member");
            return;
        }
        if((<HTMLInputElement>document.getElementById("chatNameInput")).value === "" && this.chatUsers.length > 1){
            this.setErrorMessage("Enter group chat name");
            return;
        }        

        this._newChatService.createChat(this.chatUsers).subscribe();
        //this.router.navigate(['/chats']);
				this.router.navigateByUrl('/', { skipLocationChange: true }).then(() =>
			this.router.navigate(['/chats']));
    }

    private setErrorMessage(message: string){
        let errorDiv = document.getElementById("error-message")!
        errorDiv.innerText = message;
        errorDiv.style.visibility = "visible";
        errorDiv.style.display = "block";
    }
}