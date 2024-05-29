import { Injectable } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CarrelloDto, CarrelloService } from '@proxy/carrelli';

@Injectable({
  providedIn: 'root'
})
export class ControlliCarrelloService {

  constructor(
    private carrelloService: CarrelloService,
  ) { }
  id :string;
  controlloCarrello(id: string): string{
    this.id = undefined;
    this.carrelloService.getByUserId(id).subscribe(carrel =>{
      if(carrel){
        this.id = carrel.id;
      }
      else{
        this.CreateCarrello(id);
      }
    })
    return this.id
  }
  CreateCarrello(id: string){
    this.carrelloService.create({
      userId: id,
      numDif: 0,
      prodottiNames: [],
      prodottiNum: [],
    }).subscribe(()=>{
      this.carrelloService.getByUserId(id).subscribe(carre =>{
        this.id = carre.id;
        
      })
    })
  }
}
