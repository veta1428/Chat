import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTableModule} from '@angular/material/table';
import { ChatComponent } from './chats/chats.component';
import { ChatService } from './chats/chats.service';
import { UsersComponent } from './users/users.component';
import { UserService } from './users/users.service';
import { AuthGuard } from './auth/auth-guard';
import { ChatMessagesService } from './chat-messages/chat-messages.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    ChatComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatTableModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: ChatComponent,  canActivate: [AuthGuard] },
      { path: 'chats', component: ChatComponent,  canActivate: [AuthGuard]},
      { path: 'users', component: UsersComponent, canActivate: [AuthGuard] },
    ]),
    BrowserAnimationsModule
  ],
  providers: [ChatService, UserService, ChatMessagesService],
  bootstrap: [AppComponent]
})
export class AppModule { }
