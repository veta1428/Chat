import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { LibraryService } from './library/library.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LibraryComponent } from './library/library.component';
import {MatTableModule} from '@angular/material/table';
import { BookService } from './book/book.service';
import { BookComponent } from './book/book.component';
import { ChatComponent } from './chats/chats.component';
import { ChatService } from './chats/chats.service';
import { UsersComponent } from './users/users.component';
import { UserService } from './users/users.service';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    LibraryComponent,
    BookComponent,
    ChatComponent,
    UsersComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    MatTableModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: LibraryComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'libraries', component: LibraryComponent },
      { path: 'chats', component: ChatComponent },
      { path: 'users', component: UsersComponent },
      { path: 'books/:libraryId', component: BookComponent }
    ]),
    BrowserAnimationsModule
  ],
  providers: [LibraryService, BookService, ChatService, UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
