import { ConfigStateService, ListService } from '@abp/ng.core';
import { ConfirmationService } from '@abp/ng.theme.shared';
import { Component, DoCheck, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CarrelloDto, CarrelloService } from '@proxy/carrelli';
import { ImmaginiService } from '@proxy/controllers';
import { ProdottoService } from '@proxy/prodotti';
import { ControlliCarrelloService } from 'src/service/controlli-carrello.service';

@Component({
  selector: 'app-carrello',
  templateUrl: './carrello.component.html',
  styleUrl: './carrello.component.scss',
  providers :[ ListService]
})
export class CarrelloComponent implements OnInit, DoCheck{

  selectedCarrello = {} as CarrelloDto
  prodotti=[];
  ListaProva = [];
  formCarrello: FormGroup

  constructor(
    public readonly list: ListService,
    private prodottoService: ProdottoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private immagineService: ImmaginiService,
    private carrelloService: CarrelloService,
    private config: ConfigStateService,
    private controlService: ControlliCarrelloService
  ) {}
  ngDoCheck(): void {
    if(this.giro){
      //controllo se la lista dei curriculum cambia così ricarico le immagini
      if(this.selectedCarrello.prodottiNames != this.ListaProva){
        this.RicaricaFoto()
        this.giro = false
      }
    }
    if(this.altroGiro){
      if(this.controlService.id!=undefined){
        this.carrelloService.get(this.controlService.id).subscribe(car=>{
          this.selectedCarrello = car;
          this.prodotti = this.selectedCarrello.prodottiNames;
          this.RicaricaFoto();
        });
        this.altroGiro=false;
      }
    }
  }
  giro: boolean
  altroGiro:boolean
  getCarUser(){
    this.config.getOne$("currentUser").subscribe(async currentUser => {
      this.controlService.controlloCarrello(currentUser.id);
      this.altroGiro=true;
    })
  }
  ngOnInit(): void {
    this.getCarUser();
  }
  buildForm(){
    this.formCarrello = this.fb.group({
      numDif: [this.selectedCarrello.numDif],
      prodottiNames: [this.selectedCarrello.prodottiNames],
      prodottiNum: [this.selectedCarrello.prodottiNum]
    })
  }
  async RicaricaFoto(){
    const sleep = (ms) => new Promise(r => setTimeout(r, ms));
    await sleep(100);
    for(let ima of this.selectedCarrello.immagini1){  
      this.immagineService.getImage(ima).subscribe(function (imageFile) {
        var src = "data:image/png;base64," + imageFile;
        this.imgElement = document.getElementById(ima) as HTMLImageElement;
        this.imgElement.src = src;
      })
    }
    this.giro = true
    this.ListaProva = this.selectedCarrello.prodottiNames
  }
  ChangeNum(num: number, ind:number){
    this.selectedCarrello.prodottiNum[ind] = num
  }
  deleteProd(ind : number){
    this.selectedCarrello.prodottiNames.splice(ind, 1)
    this.selectedCarrello.prodottiNum.splice(ind, 1)
    console.log(this.selectedCarrello.prodottiNames)
    console.log(this.selectedCarrello.prodottiNum)
    return
    this.carrelloService.update(this.selectedCarrello.id, {userId: this.selectedCarrello.userId, numDif: this.selectedCarrello.numDif, prodottiNames: this.selectedCarrello.prodottiNames, prodottiNum: this.selectedCarrello.prodottiNum}).subscribe(()=>{
      this.getCarUser();
    })
  }
}