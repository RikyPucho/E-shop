import type { CreateOrdineDto, OrdineDto, OrdineGeyListInput, UpdateOrdineDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ProdottoLookupDto } from '../prodotti/models';

@Injectable({
  providedIn: 'root',
})
export class OrdineService {
  apiName = 'Default';
  

  create = (input: CreateOrdineDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/ordine',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/ordine/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, OrdineDto>({
      method: 'GET',
      url: `/api/app/ordine/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: OrdineGeyListInput, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<OrdineDto>>({
      method: 'GET',
      url: '/api/app/ordine',
      params: { stato: input.stato, nome: input.nome, prezzo: input.prezzo, maggiore: input.maggiore, citta: input.citta, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getProdottiLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<ProdottoLookupDto>>({
      method: 'GET',
      url: '/api/app/ordine/prodotti-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateOrdineDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/ordine/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
