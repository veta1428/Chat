import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { UserService } from './users.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-users',
    templateUrl: './users.component.html',
    styleUrls: ['./users.component.scss']
})

export class UsersComponent implements OnInit{
    /**
     *
     */
    public dataSource: UserModel[] = [];
    public displayedColumns: string[] = ['firstName', 'lastName'];
    public isLoading: boolean = true;
    constructor(private _userSrevice: UserService, private _detector: ChangeDetectorRef, private router: Router) {

    }

    ngOnInit(): void {
        this._userSrevice.getUsers().subscribe((data) => {
            this.dataSource = data;
            this.isLoading = false;
            this._detector.detectChanges();
        })
    }

    onRowClicked(user: UserModel): void {
        //
        //this.router.navigate(['/chat-messages', user.userId]);
    }
}