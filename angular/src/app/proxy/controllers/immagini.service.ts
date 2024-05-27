import { RestService, Rest } from '@abp/ng.core';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ImmaginiService {
  apiName = 'Default';
  

  getImage = (name: string, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'GET',
      url: '/api/img',
      params: { name },
    },
    { apiName: this.apiName,...config });
  

  upload = (img: FormData, config?: Partial<Rest.Config>) =>
    this.restService.request<any, number[]>({
      method: 'POST',
      url: '/api/img/upload',
      body: img,
    },
    { apiName: this.apiName,...config });

  constructor(private restService: RestService) {}
}
