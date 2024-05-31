import { Component, DoCheck, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ProdottoService, ProdottoDto } from '@proxy/prodotti';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';
import { ImmaginiService } from '@proxy/controllers';
import { OrdineDto, OrdineGeyListInput, OrdineService, statiOptions } from '@proxy/ordini';
import { query } from '@angular/animations';
@Component({
  selector: 'app-ordini-table',
  templateUrl: './ordini-table.component.html',
  styleUrl: './ordini-table.component.scss',
  providers: [ListService<OrdineGeyListInput>]
})
export class OrdiniTableComponent implements OnInit{
  ordini = { items: [], totalCount: 0} as PagedResultDto<OrdineDto>;

  isModalOpen = false;
  isModalStato = false;

  form: FormGroup;
  filtri: FormGroup;

  selectedOrdine = {} as OrdineDto;

  filtro: OrdineGeyListInput;
  stati = statiOptions;

  constructor(
    public readonly list: ListService,
    private ordineService: OrdineService,
    private confirmation : ConfirmationService,
    private fb : FormBuilder,
    private immagineService: ImmaginiService
  ){}
  cerca(){
    // this.filtro = this.filtri.value as OrdineGeyListInput
  }
  ngOnInit(): void {
    this.buildFiltri();
    const ordineStreamCreator = (query) => this.ordineService.getList({...query, ...this.filtri.value});

    this.list.hookToQuery(ordineStreamCreator).subscribe((response) =>{
      this.ordini = response;
    })
  }
  mag = ''
  maggiore(val: boolean){
    this.filtri.patchValue({maggiore: val});
    this.mag = val==true?'Maggiore':'Minore';
    this.list.get()
  }
  isAvanzati = false;
  filAvanzati(){
    this.isAvanzati = !this.isAvanzati;
    this.maggiore(true)
  }
  clickStato(id:string){
    this.ordineService.get(id).subscribe((ord)=>{
      this.selectedOrdine = ord;
      this.buildForm();
      this.isModalStato=true;
    })
    
  }
  buildFiltri(){
    this.filtri = this.fb.group({
      stato: [null],
      nome: [null],
      prezzo: [null],
      maggiore: [null],
      citta: [null]
    })
  }
  changeStato(num: number){
    this.form.patchValue({stato: num})
    this.save();
    this.isModalStato=false;
    this.list.get();
  }

  edit(id: string){
    this.ordineService.get(id).subscribe((ord)=>{
      this.selectedOrdine = ord;
      this.buildForm();
      this.isModalOpen=true;
    })
  }
  buildForm(){
    this.form = this.fb.group({
      cognome: [this.selectedOrdine.cognome],
      telefono: [this.selectedOrdine.telefono],
      provincia: [this.selectedOrdine.provincia],
      indrizzo: [ this.selectedOrdine.indrizzo],
      cap: [this.selectedOrdine.cap],
      citta: [this.selectedOrdine.citta],
      stato: [this.selectedOrdine.stato]
    })
  }

  save(){
    if(this.form.invalid){
      return;
    }
    if(this.selectedOrdine.id){
      this.ordineService.update(this.selectedOrdine.id, this.form.value).subscribe(()=>{
        this.isModalOpen=false;
        this.form.reset();
        this.list.get();
      })
    }
  }

  delete(id:string){
    this.confirmation.warn('::AreYouSureToDelete', '::AreYouSure')
        .subscribe((status) => {
          if (status === Confirmation.Status.confirm) {
            this.ordineService.delete(id).subscribe(() => this.list.get());
          }
	    });
  }
}
