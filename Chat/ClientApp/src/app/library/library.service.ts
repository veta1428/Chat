import { Inject, Injectable } from "@angular/core";
import { LibraryModel } from "../models/LibraryModel";
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";

@Injectable({
    providedIn: 'root',
  })
export class LibraryService {
    private baseUrl: string;

    constructor(private _client: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.baseUrl = baseUrl;
    }
    getLibraries() : Observable<LibraryModel[]>{
        return this._client.get<LibraryModel[]>('library/get-libraries');
    }
}