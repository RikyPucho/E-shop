import { Component, DoCheck, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ProdottoService, ProdottoDto, GetProdottoListDto } from '@proxy/prodotti';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ImmaginiService } from '@proxy/controllers';
import { v4 as uuidv4 } from 'uuid';

@Component({
  selector: 'app-prodotti-def',
  templateUrl: './prodotti-def.component.html',
  styleUrl: './prodotti-def.component.scss',
  providers: [ListService<GetProdottoListDto>]
})
export class ProdottiDefComponent implements OnInit, DoCheck {
  prodotto = { items: [], totalCount: 0 } as PagedResultDto<ProdottoDto>;

  isModalOpen = false;

  form: FormGroup;
  filtri: FormGroup;

  selectedProdotto = {} as ProdottoDto;

  constructor(
    public readonly list: ListService,
    private prodottoService: ProdottoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private immagineService: ImmaginiService
  ) {}

  idsImg: string[] = [];
  onFileSelected(files: FileList) {
    var fileToUpload: File | null = null;
    var guid =  uuidv4()
    fileToUpload = files.item(0);
    this.uploadImg(guid, fileToUpload);
  } 
  uploadImg(guid: string, fileToUpload: File){
    var formData: FormData = new FormData();
    formData.append('img', fileToUpload, guid);
    this.idsImg.push(guid);
    this.immagineService.upload(formData).subscribe()
  }

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
          this.imgElement = document.getElementById(value.immagine1) as HTMLImageElement;
          this.imgElement.src = src;
        })
      }
      if(value.immagine2 != '' && value.immagine2!=null){
        this.immagineService.getImage(value.immagine2).subscribe(function (imageFile) {
          var src = "data:image/png;base64," + imageFile;
          this.imgElement = document.getElementById(value.immagine2) as HTMLImageElement;
          this.imgElement.src = src;
        })
      }
      if(value.immagine3 != '' && value.immagine3!=null){
        this.immagineService.getImage(value.immagine3).subscribe(function (imageFile) {
          var src = "data:image/png;base64," + imageFile;
          this.imgElement = document.getElementById(value.immagine3) as HTMLImageElement;
          this.imgElement.src = src;
        })
      }
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
    this.buildFiltri();
    this.maggiore(true);
    const prodottoStreamCreator = (query) => this.prodottoService.getList({...query, ...this.filtri.value});

    this.list.hookToQuery(prodottoStreamCreator).subscribe((response) => {
      this.prodotto = response;
    });
    this.RicaricaFoto();
  }
  mag = ''
  maggiore(val: boolean){
    this.filtri.patchValue({maggiore: val});
    this.mag = val==true?'Maggiore':'Minore';
    this.list.get()
  }
  buildFiltri(){
    this.filtri  = this.fb.group({
      nome:[null],
      maggiore:[null],
      prezzo:[null]
    })
  }
  createProdotto() {
    this.idsImg = []
    this.selectedProdotto = {} as ProdottoDto;
    this.buildForm();
    this.isModalOpen = true;
  }
  
  editProdotto(id: string) {
    this.idsImg = []
    this.prodottoService.get(id).subscribe((prodotto) => {
      this.selectedProdotto = prodotto;
      this.buildForm();
      this.isModalOpen = true;
      this.idsImg.push(this.selectedProdotto.immagine1)
      if(this.selectedProdotto.immagine2!=''){
        this.idsImg.push(this.selectedProdotto.immagine2)
      }
      if(this.selectedProdotto.immagine3!=''){
        this.idsImg.push(this.selectedProdotto.immagine3)
      }
    });
  }

  buildForm() {
    this.form = this.fb.group({
      nome: [this.selectedProdotto.nome || '', Validators.required],
      des: [this.selectedProdotto.des || ''],
      prezzo: [this.selectedProdotto.prezzo || '', Validators.required],
      immagine1: [this.selectedProdotto.immagine1 || '', Validators.required],
      immagine2: [this.selectedProdotto.immagine2 || '', Validators.required],
      immagine3: [this.selectedProdotto.immagine3 || '', Validators.required]
    });
  }
  invalido(text: string){
    this.confirmation.warn('Il campo '+ text+' è obbligatorio', 'Errore nella compilazione')
  }

  save() {
    if(this.form.get('nome').value == ''){
      this.invalido('nome');
      return;
    }
    if(this.form.get('prezzo').value == ''){
      this.invalido('prezzo');
      return;
    }
    if(this.idsImg.length == 0){
      this.invalido('prima immagine')
      return;
    }
    if(this.idsImg.length == 3){
      this.form.patchValue({immagine1: this.idsImg[0], immagine2: this.idsImg[1], immagine3: this.idsImg[2]})
    }
    if(this.idsImg.length > 3){
      this.form.patchValue({immagine1: this.idsImg[this.idsImg.length-3], immagine2: this.idsImg[this.idsImg.length-2], immagine3: this.idsImg[this.idsImg.length-1]})
    }
    if(this.idsImg.length == 2){
      this.form.patchValue({immagine1: this.idsImg[0], immagine2: this.idsImg[1]})
    }
    if(this.idsImg.length == 1){
      this.form.patchValue({immagine1: this.idsImg[0]})
    }

    if (this.selectedProdotto.id) {
      this.prodottoService
        .update(this.selectedProdotto.id, this.form.value)
        .subscribe(() => {
          this.isModalOpen = false;
          this.form.reset();
          this.list.get();
        });
    } else {
      this.prodottoService.create(this.form.value).subscribe(() => {
        this.isModalOpen = false;
        this.form.reset();
        this.list.get();
      });
    }
  }

  delete(id: string) {
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
        .subscribe((status) => {
          if (status === Confirmation.Status.confirm) {
            this.prodottoService.delete(id).subscribe(() => this.list.get());
          }
	    });
  }

}
