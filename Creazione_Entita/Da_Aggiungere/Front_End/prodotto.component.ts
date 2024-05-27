import { Component, OnInit } from '@angular/core';
import { ListService, PagedResultDto } from '@abp/ng.core';
import { ProdottoService, ProdottoDto } from '@proxy/prodotti ';//togli lo spazio
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ConfirmationService, Confirmation } from '@abp/ng.theme.shared';

@Component({
  selector: 'app-prodotto ',//togli lo spazio
  templateUrl: './prodotto.component.html',
  styleUrls: ['./prodotto.component.scss'],
  providers: [ListService],
})
export class ProdottoComponent implements OnInit {
  prodotto = { items: [], totalCount: 0 } as PagedResultDto<ProdottoDto>;

  isModalOpen = false;

  form: FormGroup;

  selectedProdotto = {} as ProdottoDto;

  constructor(
    public readonly list: ListService,
    private prodottoService: ProdottoService,
    private fb: FormBuilder,
    private confirmation: ConfirmationService
  ) {}

  ngOnInit(): void {
    const prodottoStreamCreator = (query) => this.prodottoService.getList(query);

    this.list.hookToQuery(prodottoStreamCreator).subscribe((response) => {
      this.prodotto = response;
    });
  }

  createProdotto() {
    this.selectedProdotto = {} as ProdottoDto;
    this.buildForm();
    this.isModalOpen = true;
  }

  editProdotto(id: string) {
    this.prodottoService.get(id).subscribe((prodotto) => {
      this.selectedProdotto = prodotto;
      this.buildForm();
      this.isModalOpen = true;
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

  save() {
    if (this.form.invalid) {
      return;
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