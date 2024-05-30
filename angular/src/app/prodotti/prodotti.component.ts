import { Component, DoCheck, OnInit } from '@angular/core';
import { ConfigStateService, ListService, PagedResultDto } from '@abp/ng.core';
import { ProdottoService, ProdottoDto } from '@proxy/prodotti';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ImmaginiService } from '@proxy/controllers';
import { ControlliCarrelloService } from 'src/service/controlli-carrello.service';
@Component({
  selector: 'app-prodotti',
  templateUrl: './prodotti.component.html',
  styleUrl: './prodotti.component.scss',
  providers: [ListService]
})
export class ProdottiComponent implements  OnInit, DoCheck {
  prodotto = { items: [], totalCount: 0 } as PagedResultDto<ProdottoDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedProdotto = {} as ProdottoDto;

  constructor(
    public readonly list: ListService,
    private prodottoService: ProdottoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private immagineService: ImmaginiService,
    private config: ConfigStateService,
    private controlService: ControlliCarrelloService
  ) {}

  ListaProva: any;
  giro = false
  imgElement: HTMLImageElement
  async RicaricaFoto(){
    const sleep = (ms) => new Promise(r => setTimeout(r, ms));
    await sleep(100);
    for(let value of this.prodotto.items){
      this.immagineService.getImage(value.immagine1).subscribe(function (imageFile) {
        var src = "data:image/png;base64," + imageFile;
        this.imgElement = document.getElementById(value.immagine1) as HTMLImageElement;
        this.imgElement.src = src;
      })
      // this.immagineService.getImage(value.immagine2).subscribe(function (imageFile) {
      //   var src = "data:image/png;base64," + imageFile;
      //   this.imgElement = document.getElementById(value.immagine2) as HTMLImageElement;
      //   this.imgElement.src = src;
      // })
      // this.immagineService.getImage(value.immagine3).subscribe(function (imageFile) {
      //   var src = "data:image/png;base64," + imageFile;
      //   this.imgElement = document.getElementById(value.immagine3) as HTMLImageElement;
      //   this.imgElement.src = src;
      // })
    }
    this.giro = true
    this.ListaProva = this.prodotto.items
  }
  
  ngDoCheck(): void {
    if(this.giro){
      //controllo se la lista dei curriculum cambia così ricarico le immagini
      if(this.prodotto.items != this.ListaProva){
        this.RicaricaFoto()
        this.giro = false
      }
      // if(this.list.maxResultCount != this.formFiltro.get('maxRes').value){
      //   this.getCurriculums();
      //   this.RicaricaFoto();
      // }
    }
  }
  ngOnInit(): void {
    this.config.getOne$("currentUser").subscribe(currentUser => {
      this.controlService.controlloCarrello(currentUser.id)
   })
    const prodottoStreamCreator = (query) => this.prodottoService.getList(query);

    this.list.hookToQuery(prodottoStreamCreator).subscribe((response) => {
      this.prodotto = response;
    });
    this.RicaricaFoto();
  }
  
}
