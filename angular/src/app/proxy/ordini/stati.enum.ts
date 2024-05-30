import { mapEnumToOptions } from '@abp/ng.core';

export enum Stati {
  Nuovo = 0,
  InCorso = 1,
  Spedito = 2,
  Concluso = 3,
}

export const statiOptions = mapEnumToOptions(Stati);
