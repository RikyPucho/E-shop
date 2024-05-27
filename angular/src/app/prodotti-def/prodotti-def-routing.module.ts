import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProdottiDefComponent } from './prodotti-def.component';

const routes: Routes = [{ path: '', component: ProdottiDefComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProdottiDefRoutingModule { }
