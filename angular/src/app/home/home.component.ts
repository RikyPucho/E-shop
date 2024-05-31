import { AuthService, ListService, PagedResultDto } from '@abp/ng.core';
import { Component, OnInit } from '@angular/core';
import { ImmaginiService } from '@proxy/controllers';
import { ProdottoDto, ProdottoService } from '@proxy/prodotti';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  providers: [ListService]
})
export class HomeComponent implements OnInit {
  get hasLoggedIn(): boolean {
    return this.authService.isAuthenticated;
  }
  prodotti = {items: [], totalCount: 0} as PagedResultDto<ProdottoDto>
  constructor(private authService: AuthService, private prodottiService:ProdottoService, private list: ListService, private immagineService: ImmaginiService ) {}
  
  ngOnInit(): void {
    const prodottoStreamCreator = (query)=>this.prodottiService.getList(query)

    this.list.hookToQuery(prodottoStreamCreator).subscribe((response)=>{
      this.prodotti = response
      this.RicaricaFoto();
    })
  }
  ListaProva: any;
  giro = false
  imgElement: HTMLImageElement
  async RicaricaFoto(){
    const sleep = (ms) => new Promise(r => setTimeout(r, ms));
    await sleep(100);
    var i = 0;
    for(let value of this.prodotti.items){
      if(i < 3){
        this.immagineService.getImage(value.immagine1).subscribe(function (imageFile) {
          var src = "data:image/png;base64," + imageFile;
          this.imgElement = document.getElementById(value.immagine1) as HTMLImageElement;
          this.imgElement.src = src;
        })
      }
      i++;
    }
    this.giro = true
    this.ListaProva = this.prodotti.items
  }

  login() {
    this.authService.navigateToLogin();
  }
}
