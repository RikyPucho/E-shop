import { RoutesService, eLayoutType } from '@abp/ng.core';
import { APP_INITIALIZER } from '@angular/core';

export const APP_ROUTE_PROVIDER = [
  { provide: APP_INITIALIZER, useFactory: configureRoutes, deps: [RoutesService], multi: true },
];

function configureRoutes(routesService: RoutesService) {
  return () => {
    routesService.add([
      {
        path: '/',
        name: '::Menu:Home',
        iconClass: 'fas fa-home',
        order: 1,
        layout: eLayoutType.application,
      },
      {
        path: '/carrello',
        name: 'Carrello',
        iconClass: 'fas fa-shopping-cart',
        order: 2,
        layout: eLayoutType.application,
        requiredPolicy: 'Eshop.Carrelli'
      },
      {
        path: '/prodotti',
        name: 'Prodotti',
        iconClass: 'fas fa-tag',
        order: 3,
        layout: eLayoutType.application,
      },
      {
        path: '/def-prodotti',
        name: 'Definizione prodotti',
        iconClass: 'fas fa-plus',
        order: 4,
        layout: eLayoutType.application,
        requiredPolicy: 'Eshop.Prodotti'
      },
    ]);
  };
}
