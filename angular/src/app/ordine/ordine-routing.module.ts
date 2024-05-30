import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrdineComponent } from './ordine.component';

const routes: Routes = [{ path: '', component: OrdineComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrdineRoutingModule { }
