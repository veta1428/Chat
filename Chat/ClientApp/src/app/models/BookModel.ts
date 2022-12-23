export interface BookModel {
    id: number,
    title: String;
    author : String;
    genre : Genre; 
    pages : number;
}

export enum Genre {
    Fantasy = 1,
    Horror = 2,
    Science = 3,
    Sport = 4,
    Romance = 5
}