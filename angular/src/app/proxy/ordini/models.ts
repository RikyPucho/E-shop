import type { Stati } from './stati.enum';
import type { EntityDto, PagedAndSortedResultRequestDto } from '@abp/ng.core';

export interface CreateOrdineDto {
  nome?: string;
  cognome?: string;
  telefono?: string;
  provincia?: string;
  indrizzo?: string;
  cap?: string;
  citta?: string;
  prezzo: number;
  stato: Stati;
  prodottiNames: string[];
  prodottiNum: number[];
  prodottiNomi: string[];
  prodottiPrezzi: number[];
}

export interface OrdineDto extends EntityDto<string> {
  nome?: string;
  cognome?: string;
  telefono?: string;
  provincia?: string;
  indrizzo?: string;
  cap?: string;
  citta?: string;
  prezzo: number;
  stato: Stati;
  prodottiNames: string[];
  prodottiNum: number[];
  prodottiNomi: string[];
  prodottiPrezzi: number[];
}

export interface OrdineGeyListInput extends PagedAndSortedResultRequestDto {
  stato?: Stati;
  nome?: string;
  prezzo?: number;
  maggiore?: boolean;
  citta?: string;
}

export interface UpdateOrdineDto {
  cognome?: string;
  telefono?: string;
  provincia?: string;
  indrizzo?: string;
  cap?: string;
  citta?: string;
  stato: Stati;
}
