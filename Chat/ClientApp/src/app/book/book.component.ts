import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BookModel } from '../models/BookModel';
import { BookService } from './book.service';

@Component({
  selector: 'app-books',
  templateUrl: './book.component.html',
  styleUrls: ['./book.component.scss']
})
export class BookComponent implements OnInit{
    /**
     *
     */
    public dataSource: BookModel[] = [];
    public displayedColumns: string[] = ['title', 'author', /*'genre',*/ 'pages'];
    public libraryId: number;
    public isLoading = true;
    constructor(private _bookSrevice: BookService, private _detector: ChangeDetectorRef,
        private _route: ActivatedRoute) {
        this.libraryId = +(_route.snapshot.paramMap.get('libraryId')!);
    }

    ngOnInit(): void {
        this._bookSrevice.getBooksByLibraryId(this.libraryId).subscribe((data) =>{
            this.dataSource = data;
            this.isLoading = false;
            this._detector.detectChanges();
        })
    } 

    onRowClicked(book: BookModel): void {
        this._bookSrevice.getBook(book.id).subscribe(res=>{
            var fileURL = URL.createObjectURL(res);
            window.open(fileURL);
        });

        console.log(book);
    }
}
