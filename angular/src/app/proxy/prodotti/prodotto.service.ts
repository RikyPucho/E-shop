import type { CreateProdottoDto, GetProdottoListDto, ProdottoDto, UpdateProdottoDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProdottoService {
  apiName = 'Default';
  

  create = (input: CreateProdottoDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProdottoDto>({
      method: 'POST',
      url: '/api/app/prodotto',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/prodotto/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, ProdottoDto>({
      method: 'GET',
      url: `/api/app/prodotto/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetProdottoListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<ProdottoDto>>({
      method: 'GET',
      url: '/api/app/prodotto',
      params: { nome: input.nome, prezzo: input.prezzo, maggiore: input.maggiore, sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateProdottoDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/prodotto/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
