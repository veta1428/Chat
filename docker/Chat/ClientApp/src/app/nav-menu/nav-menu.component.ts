import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth/auth.service';
import { UserModel } from '../models/UserModel';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {
  /**
   *
   */
  public userModel: UserModel | null = null;
  
  constructor(private _authService: AuthService) {

  }

  ngOnInit(): void {
    this._authService.getUserInfo().subscribe((user) =>{
      this.userModel = user;
    });
  }

  isExpanded = false;

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  onLogoutClicked() {
    this._authService.logout();
  }
}
