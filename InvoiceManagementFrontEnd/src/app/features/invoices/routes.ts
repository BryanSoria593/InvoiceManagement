import { Routes } from '@angular/router';
import { InvoicesComponent } from './invoices.component';

export default [
  { path: '', component: InvoicesComponent },
  {
    path: 'create',
    loadComponent: () => import('./create-or-edit-invoice/create-or-edit-invoice.component')
      .then(m => m.CreateOrEditInvoiceComponent)
  },
  {
    path: 'create/:id',
    loadComponent: () => import('./create-or-edit-invoice/create-or-edit-invoice.component')
      .then(m => m.CreateOrEditInvoiceComponent)
  }
] satisfies Routes;

