import { Component, DoCheck, OnInit } from '@angular/core';
import { ConfigStateService, ListService, PagedResultDto } from '@abp/ng.core';
import { ProdottoService, ProdottoDto } from '@proxy/prodotti';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation, ToasterService } from '@abp/ng.theme.shared';
import { ImmaginiService } from '@proxy/controllers';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';
import { ControlliCarrelloService } from 'src/service/controlli-carrello.service';
import { CarrelloDto, CarrelloService } from '@proxy/carrelli';
@Component({
  selector: 'app-prodotto',
  templateUrl: './prodotto.component.html',
  styleUrl: './prodotto.component.scss',
  providers: [ListService]
})
export class ProdottoComponent implements OnInit, DoCheck {
  prodotto = { items: [], totalCount: 0 } as PagedResultDto<ProdottoDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedProdotto = {} as ProdottoDto;
  selectedCarrello = {} as CarrelloDto;

  constructor(
    public readonly list: ListService,
    private prodottoService: ProdottoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private immagineService: ImmaginiService,
    private route: ActivatedRoute,
    private config: ConfigStateService,
    private controlService: ControlliCarrelloService,
    private carrelloService: CarrelloService,
    private router: Router,
    private toaster: ToasterService
  ) {}

  ListaProva: any;
  giro = false
  imgElement: HTMLImageElement
  async RicaricaFoto(){
    const sleep = (ms) => new Promise(r => setTimeout(r, ms));
    await sleep(100);
    for(let value of this.prodotto.items){
        if(value.immagine1 != '' && value.immagine1!=null){
          this.immagineService.getImage(value.immagine1).subscribe(function (imageFile) {
            var src = "data:image/png;base64," + imageFile;
            this.imgElement = document.getElementsByName(value.immagine1);
            for(let img of this.imgElement){
              img.src = src;
            }
          })
        }
        if(value.id==this.selectedProdotto.id){
          if(value.immagine2 != '' && value.immagine2!=null){
            this.immagineService.getImage(value.immagine2).subscribe(function (imageFile) {
              var src = "data:image/png;base64," + imageFile;
              this.imgElement = document.getElementsByName(value.immagine2);
              for(let img of this.imgElement){
                img.src = src;
              }
            })
          }
          if(value.immagine3 != '' && value.immagine3!=null){
            this.immagineService.getImage(value.immagine3).subscribe(function (imageFile) {
              var src = "data:image/png;base64," + imageFile;
              this.imgElement = document.getElementsByName(value.immagine3);
              for(let img of this.imgElement){
                img.src = src;
              }
            })
          }
        }
    }
      // if(this.selectedProdotto.immagine2 != '' && this.selectedProdotto.immagine2!=null){
      //   this.immagineService.getImage(this.selectedProdotto.immagine2).subscribe(function (imageFile) {
      //     var src = "data:image/png;base64," + imageFile;
      //     this.imgElement = document.getElementById(this.selectedProdotto.immagine2) as HTMLImageElement;
      //     this.imgElement.src = src;
      //   })
      // }
      // if(this.selectedProdotto.immagine3 != '' && this.selectedProdotto.immagine3!=null){
      //   this.immagineService.getImage(this.selectedProdotto.immagine3).subscribe(function (imageFile){
      //     var src = "data:image/png;base64," + imageFile;
      //     this.imgElement = document.getElementById(this.selectedProdotto.immagine3) as HTMLImageElement;
      //     this.imgElement.src = src;
      //   })
      // }
      
    this.giro = true
    this.ListaProva = this.prodotto.items
  }
  altroGiro:boolean
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
    if(this.altroGiro){
      if(this.controlService.id!=undefined){
        this.carrelloService.get(this.controlService.id).subscribe(car=>{
          this.selectedCarrello = car;
        });
        this.altroGiro=false;
      }
    }
  }
  ChangeNum(num: number){
    if(num> 0){
      this.numProd = num
    }
  }
  ChangeImg(num: number){
    this.immag = num;
    this.RicaricaFoto();
  }
  immag: number;
  numProd: number;
  CarrelloId: string;
  ngOnInit(): void {
    this.altroGiro =true;
    this.config.getOne$("currentUser").subscribe(async currentUser => {
      this.controlService.controlloCarrello(currentUser.id);
    })
    this.numProd = 1
    this.immag = 1
    this.route.paramMap.subscribe((param: ParamMap)=>{
      const id = (param.get('id')!)
      this.prodottoService.get(id).subscribe((prod)=>{
        this.selectedProdotto = prod;
      })
    })
    const prodottoStreamCreator = (query) => this.prodottoService.getList(query);

    this.list.hookToQuery(prodottoStreamCreator).subscribe((response) => {
      this.prodotto = response;
    });
    this.RicaricaFoto();
  }
  AddCarrello(){
    if(this.selectedCarrello.prodottiNames== null){
      this.selectedCarrello.prodottiNames = []
    }
    if(this.selectedCarrello.prodottiNum == null){
      this.selectedCarrello.prodottiNum = []
    }
    if(this.selectedCarrello.prodottiNames.includes(this.selectedProdotto.id.toUpperCase())){
      var ind = this.selectedCarrello.prodottiNames.indexOf(this.selectedProdotto.id.toUpperCase())
      this.selectedCarrello.prodottiNames.splice(ind, 1)
      this.selectedCarrello.prodottiNum.splice(ind, 1)
    }
    this.selectedCarrello.prodottiNames.push(this.selectedProdotto.id);
    this.selectedCarrello.prodottiNum.push(this.numProd);
    console.log(this.selectedCarrello)
    this.carrelloService.update(this.selectedCarrello.id, {userId:this.selectedCarrello.userId, numDif:this.selectedCarrello.numDif, prodottiNames: this.selectedCarrello.prodottiNames, prodottiNum: this.selectedCarrello.prodottiNum}).subscribe(()=>{
      this.toaster.success(this.numProd +(this.numProd == 1 ? ' prodotto: ': ' prodotti: ')+'"'+ this.selectedProdotto.nome+ (this.numProd == 1 ? '" aggiunto': '" aggiunti')+' al carrello', 'Carrello')
      // this.confirmation.info(this.numProd +(this.numProd == 1 ? ' prodotto: ': ' prodotti: ')+'"'+ this.selectedProdotto.nome+ (this.numProd == 1 ? '" aggiunto': '" aggiunti')+' al carrello', 'Carrello', this.option).subscribe(status=>{
      //   if(status== Confirmation.Status.reject){
      //     this.router.navigate(['/carrello']);
      //   }
      // })
    })
  }
}
