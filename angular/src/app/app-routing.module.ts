import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProdottoComponent } from './prodotto/prodotto.component';

const routes: Routes = [
  {
    path: '',
    pathMatch: 'full',
    loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
  },
  {
    path: 'account',
    loadChildren: () => import('@abp/ng.account').then(m => m.AccountModule.forLazy()),
  },
  {
    path: 'identity',
    loadChildren: () => import('@abp/ng.identity').then(m => m.IdentityModule.forLazy()),
  },
  {
    path: 'tenant-management',
    loadChildren: () =>
      import('@abp/ng.tenant-management').then(m => m.TenantManagementModule.forLazy()),
  },
  {
    path: 'setting-management',
    loadChildren: () =>
      import('@abp/ng.setting-management').then(m => m.SettingManagementModule.forLazy()),
  },
  { path: 'def-prodotti', loadChildren: () => import('./prodotti-def/prodotti-def.module').then(m => m.ProdottiDefModule) },
  { path: 'prodotti', loadChildren: () => import('./prodotti/prodotti.module').then(m => m.ProdottiModule)},
  {path: 'prodotti/:id', loadChildren: () => import('./prodotto/prodotto.module').then(m => m.ProdottoModule)},
  { path: 'carrello', loadChildren: () => import('./carrello/carrello.module').then(m => m.CarrelloModule) },
  { path: 'ordine', loadChildren: () => import('./ordine/ordine.module').then(m => m.OrdineModule) }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {})],
  exports: [RouterModule],
})
export class AppRoutingModule {}
