import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Eshop',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44341/',
    redirectUri: baseUrl,
    clientId: 'Eshop_App',
    responseType: 'code',
    scope: 'offline_access Eshop',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44341',
      rootNamespace: 'Eshop',
    },
  },
} as Environment;
