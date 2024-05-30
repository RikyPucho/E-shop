import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CarrelloDto extends EntityDto<string> {
  userId?: string;
  numDif: number;
  prodottiNames: string[];
  prodottiNum: number[];
  prodottiNomi: string[];
  prodottiPrezzi: number[];
  immagini1: string[];
  immagini2: string[];
  immagini3: string[];
}

export interface CreateCarrelloDto {
  userId?: string;
  numDif: number;
  prodottiNames: string[];
  prodottiNum: number[];
}

export interface GetCarrelloListDto extends PagedAndSortedResultRequestDto {
}

export interface UpdateCarrelloDto {
  userId?: string;
  numDif: number;
  prodottiNames: string[];
  prodottiNum: number[];
}
