import type { CarrelloDto, CreateCarrelloDto, GetCarrelloListDto, UpdateCarrelloDto } from './models';
import { RestService, Rest } from '@abp/ng.core';
import type { ListResultDto, PagedResultDto } from '@abp/ng.core';
import { Injectable } from '@angular/core';
import type { ProdottoLookupDto } from '../prodotti/models';

@Injectable({
  providedIn: 'root',
})
export class CarrelloService {
  apiName = 'Default';
  

  create = (input: CreateCarrelloDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'POST',
      url: '/api/app/carrello',
      body: input,
    },
    { apiName: this.apiName,...config });
  

  delete = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'DELETE',
      url: `/api/app/carrello/${id}`,
    },
    { apiName: this.apiName,...config });
  

  get = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CarrelloDto>({
      method: 'GET',
      url: `/api/app/carrello/${id}`,
    },
    { apiName: this.apiName,...config });
  

  getByUserId = (id: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, CarrelloDto>({
      method: 'GET',
      url: `/api/app/carrello/${id}/by-user-id`,
    },
    { apiName: this.apiName,...config });
  

  getList = (input: GetCarrelloListDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, PagedResultDto<CarrelloDto>>({
      method: 'GET',
      url: '/api/app/carrello',
      params: { sorting: input.sorting, skipCount: input.skipCount, maxResultCount: input.maxResultCount },
    },
    { apiName: this.apiName,...config });
  

  getProdottoLookup = (config?: Partial<Rest.Config>) =>
    this.restService.request<any, ListResultDto<ProdottoLookupDto>>({
      method: 'GET',
      url: '/api/app/carrello/prodotto-lookup',
    },
    { apiName: this.apiName,...config });
  

  update = (id: string, input: UpdateCarrelloDto, config?: Partial<Rest.Config>) =>
    this.restService.request<any, void>({
      method: 'PUT',
      url: `/api/app/carrello/${id}`,
      body: input,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
