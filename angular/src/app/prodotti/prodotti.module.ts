import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { ProdottiRoutingModule } from './prodotti-routing.module';
import { ProdottiComponent } from './prodotti.component';


@NgModule({
  declarations: [
    ProdottiComponent
  ],
  imports: [
    SharedModule,
    ProdottiRoutingModule
  ]
})
export class ProdottiModule { }
