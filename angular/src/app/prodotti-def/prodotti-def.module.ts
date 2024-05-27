import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { ProdottiDefRoutingModule } from './prodotti-def-routing.module';
import { ProdottiDefComponent } from './prodotti-def.component';


@NgModule({
  declarations: [
    ProdottiDefComponent
  ],
  imports: [
    SharedModule,
    ProdottiDefRoutingModule
  ]
})
export class ProdottiDefModule { }
