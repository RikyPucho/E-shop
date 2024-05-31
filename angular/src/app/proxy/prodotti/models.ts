import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateProdottoDto {
  nome: string;
  des?: string;
  prezzo: number;
  immagine1?: string;
  immagine2?: string;
  immagine3?: string;
}

export interface GetProdottoListDto extends PagedAndSortedResultRequestDto {
  nome?: string;
  prezzo?: number;
  maggiore?: boolean;
}

export interface ProdottoDto extends EntityDto<string> {
  nome?: string;
  des?: string;
  prezzo: number;
  immagine1?: string;
  immagine2?: string;
  immagine3?: string;
}

export interface UpdateProdottoDto {
  nome: string;
  des?: string;
  prezzo: number;
  immagine1?: string;
  immagine2?: string;
  immagine3?: string;
}

export interface ProdottoLookupDto extends EntityDto<string> {
  nome?: string;
  des?: string;
  prezzo: number;
  immagine1?: string;
  immagine2?: string;
  immagine3?: string;
}
