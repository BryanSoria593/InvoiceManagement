
import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        pathMatch: 'full',
        redirectTo: 'login'
    },
    {
        path: 'login',
        loadComponent: () =>
            import('./features/auth/login/login.component')
                .then(m => m.LoginComponent)
    },
    {
        path: 'register',
        loadComponent: () =>
            import('./features/auth/register/register.component')
                .then(m => m.RegisterComponent)
    },
    {
        path: 'dashboard',
        loadComponent: () =>
            import('./features/dashboard/dashboard.component')
                .then(m => m.DashboardComponent)
    },
    {
        path: 'products',
        loadComponent: () =>
            import('./features/products/products.component')
                .then(m => m.ProductsComponent)
    },
    {
        path: 'customers',
        loadComponent: () =>
            import('./features/customers/customers.component')
                .then(m => m.CustomersComponent)
    },
    {
        path: 'invoices',
        loadComponent: () =>
            import('./features/invoices/invoices.component')
                .then(m => m.InvoicesComponent)
    },
    {
        path: 'configuration',
        loadComponent: () =>
            import('./features/configuration/configuration.component')
                .then(m => m.ConfigurationComponent)
    },
    {
        path: '**',
        redirectTo: 'login'
    }
];

