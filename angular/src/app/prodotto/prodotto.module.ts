import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { ProdottoRoutingModule } from './prodotto-routing.module';
import { ProdottoComponent } from './prodotto.component';


@NgModule({
  declarations: [
    ProdottoComponent
  ],
  imports: [
    SharedModule,
    ProdottoRoutingModule
  ]
})
export class ProdottoModule { }
