import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class OrdineCarrelloService {

  constructor() { }

  prodottiId: string[];
  prodottiNomi: string[];
  prodottiNum: number[];
  prodottiPrezzi: number[];
  tot: number;
  carrelloId:string;
  UserId: string;
  setOrdine(
    prodottiId: string[],
    prodottiNomi: string[],
    prodottiNum: number[],
    prodottiPrezzi: number[],
    tot: number
  ){
    this.prodottiId = prodottiId;
    this.prodottiNomi = prodottiNomi;
    this.prodottiNum = prodottiNum;
    this.prodottiPrezzi = prodottiPrezzi;
    this.tot = tot;
  }

  setCarrelloId(id:string, userId: string){
    this.carrelloId = id
    this.UserId = userId
  }
}
