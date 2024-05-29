import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { CarrelloRoutingModule } from './carrello-routing.module';
import { CarrelloComponent } from './carrello.component';


@NgModule({
  declarations: [
    CarrelloComponent
  ],
  imports: [
    SharedModule,
    CarrelloRoutingModule
  ]
})
export class CarrelloModule { }
