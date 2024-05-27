import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProdottoComponent } from './prodotto.component';

const routes: Routes = [{ path: '', component: ProdottoComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdottoRoutingModule { }
