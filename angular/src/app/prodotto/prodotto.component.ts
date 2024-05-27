import { Component, DoCheck, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ProdottoService, ProdottoDto } from '@proxy/prodotti';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ImmaginiService } from '@proxy/controllers';
import { ActivatedRoute, ParamMap } from '@angular/router';
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

  constructor(
    public readonly list: ListService,
    private prodottoService: ProdottoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private immagineService: ImmaginiService,
    private route: ActivatedRoute
  ) {}

  ListaProva: any;
  giro = false
  imgElement: HTMLImageElement
  async RicaricaFoto(){
    const sleep = (ms) => new Promise(r => setTimeout(r, ms));
    await sleep(100);
    for(let value of this.prodotto.items){
      console.log(value.nome)
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
  ngOnInit(): void {
    this.numProd = 1
    this.immag = 1
    this.route.paramMap.subscribe((param: ParamMap)=>{
      const id = (param.get('id')!)
      console.log(id)
      this.prodottoService.get(id).subscribe((prod)=>{
        this.selectedProdotto = prod;
        console.log(prod)
      })
    })
    const prodottoStreamCreator = (query) => this.prodottoService.getList(query);

    this.list.hookToQuery(prodottoStreamCreator).subscribe((response) => {
      this.prodotto = response;
    });
    this.RicaricaFoto();
  }
}
