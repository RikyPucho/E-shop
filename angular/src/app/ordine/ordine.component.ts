import { ConfirmationService, ToasterService } from '@abp/ng.theme.shared';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, MaxValidator, MinValidator, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CarrelloService } from '@proxy/carrelli';
import { OrdineDto, OrdineService } from '@proxy/ordini';
import { OrdineCarrelloService } from 'src/service/ordine-carrello.service';

@Component({
  selector: 'app-ordine',
  templateUrl: './ordine.component.html',
  styleUrl: './ordine.component.scss'
})
export class OrdineComponent  implements OnInit{
  
  form: FormGroup;
  
  selectedOrdine = {} as OrdineDto;
  

  constructor(
    private fb: FormBuilder,
    private confirmation: ConfirmationService,
    private ordineSer: OrdineCarrelloService,
    private ordineService: OrdineService,
    private router: Router,
    private carrelloServioce: CarrelloService,
    private toaster: ToasterService
  ){}


  carrelloId: string;
  userId: string;
  ngOnInit(): void {
    this.Create();
    if(this.ordineSer.prodottiId == undefined || this.ordineSer.carrelloId == undefined){
      this.router.navigate(['/carrello'])
    }
    this.carrelloId = this.ordineSer.carrelloId;
    this.userId = this.ordineSer.UserId;
  }

  Create(){
    this.buildForm();
  }

  buildForm(){
    this.form = this.fb.group({
      nome: ['', Validators.required],
      cognome: ['', Validators.required],
      telefono: ['', Validators.required],
      provincia: ['', Validators.required],
      indrizzo: ['', Validators.required],
      cap: ['', [Validators.maxLength(5),Validators.minLength(5), Validators.required]],
      citta: ['', Validators.required],
      prezzo:[this.ordineSer.tot],
      stato: [0],
      prodottiNames: [this.ordineSer.prodottiId],
      prodottiNum: [this.ordineSer.prodottiNum],
      prodottiNomi: [this.ordineSer.prodottiNomi],
      prodottiPrezzi: [this.ordineSer.prodottiPrezzi],
    })
  }
  

  Save(){
    if(this.form.invalid){
      return
    }
    if(this.selectedOrdine.id){
      // this.ordineService.update(this.selectedOrdine.id, this.formUpdate).subscribe(()=>{
        
      // })
    } else{
      this.ordineService.create(this.form.value).subscribe(() =>{
        this.carrelloServioce.update(this.carrelloId, {userId: this.userId, numDif: 0, prodottiNames: [], prodottiNum: []}).subscribe(()=>{
          this.toaster.success('Ordine inviato!', 'Ordine')
          this.form.reset();
          this.router.navigate(['/carrello'])
        })
      })
    }
  }
}
