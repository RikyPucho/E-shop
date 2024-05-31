import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/shared.module';

import { OrdiniTableRoutingModule } from './ordini-table-routing.module';
import { OrdiniTableComponent } from './ordini-table.component';


@NgModule({
  declarations: [
    OrdiniTableComponent
  ],
  imports: [
    SharedModule,
    OrdiniTableRoutingModule
  ]
})
export class OrdiniTableModule { }
