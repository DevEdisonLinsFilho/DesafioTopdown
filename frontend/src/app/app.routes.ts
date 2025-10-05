import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'products',
    pathMatch: 'full'
  },
  {
    path: 'products',
    loadComponent: () =>
      import('./features/products/product-list/product-list').then(m => m.ProductListComponent)
  },
  {
    path: 'products/new',
    loadComponent: () => import('./features/products/product-form/product-form').then(m => m.ProductFormComponent)
  },
  {
    path: 'products/edit/:id',
    loadComponent: () => import('./features/products/product-form/product-form').then(m => m.ProductFormComponent)
  },
  {
    path: 'customers',
    loadComponent: () => import('./features/customers/customer-list/customer-list').then(m => m.CustomerListComponent)
  },
  {
    path: 'customers/new',
    loadComponent: () => import('./features/customers/customer-form/customer-form').then(m => m.CustomerFormComponent)
  },
  {
    path: 'customers/edit/:id',
    loadComponent: () => import('./features/customers/customer-form/customer-form').then(m => m.CustomerFormComponent)
  },
  {
    path: 'orders/new',
    loadComponent: () => import('./features/orders/order-form/order-form').then(m => m.OrderFormComponent)
  }
];