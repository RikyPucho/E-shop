import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { OrdineRoutingModule } from './ordine-routing.module';
import { OrdineComponent } from './ordine.component';


@NgModule({
  declarations: [
    OrdineComponent
  ],
  imports: [
    SharedModule,
    OrdineRoutingModule
  ]
})
export class OrdineModule { }
