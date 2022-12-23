import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { LibraryModel } from '../models/LibraryModel';
import { LibraryService } from './library.service';

@Component({
  selector: 'app-libraries',
  templateUrl: './library.component.html',
  styleUrls: ['./library.component.scss']
})
export class LibraryComponent implements OnInit{
    /**
     *
     */
    public dataSource: LibraryModel[] = [];
    public displayedColumns: string[] = ['name', 'description', 'address', 'phone', 'email'];
    public isLoading: boolean = true;
    constructor(private _librarySrevice: LibraryService, private _detector: ChangeDetectorRef) {
        
    }

    ngOnInit(): void {
        this._librarySrevice.getLibraries().subscribe((data) =>{
            this.dataSource = data;
            this.isLoading = false;
            this._detector.detectChanges();
        })
    } 

    onRowClicked(library: LibraryModel): void{
        location.href = `books/${library.id}`;
        console.log('row clicked');
        console.log(library);
    }
}
