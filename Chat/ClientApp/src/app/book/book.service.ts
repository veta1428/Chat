import { Inject, Injectable } from "@angular/core";
import { LibraryModel } from "../models/LibraryModel";
import { HttpClient } from '@angular/common/http';
import { map, Observable } from "rxjs";
import { BookModel } from "../models/BookModel";

@Injectable({
    providedIn: 'root',
  })
export class BookService {
    private baseUrl: string;

    constructor(private _client: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }
    getBooksByLibraryId(libraryId: number) : Observable<BookModel[]>{
        return this._client.get<BookModel[]>(this.baseUrl + `book/get-books/${libraryId}`);
    }
    getBook(bookId: number): Observable<Blob>{
        return this._client.get(`book/get-book-pdf/${bookId}`, { responseType: 'blob'}).pipe(
            map(res => {
                return new Blob([res], { type: 'application/pdf' })
            })
        );
    }
}