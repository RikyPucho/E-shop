import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdiniTableComponent } from './ordini-table.component';

const routes: Routes = [{ path: '', component: OrdiniTableComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrdiniTableRoutingModule { }
