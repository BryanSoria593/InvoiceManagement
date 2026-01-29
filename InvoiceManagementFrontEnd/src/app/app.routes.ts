
import { Routes } from '@angular/router';
import { authGuard } from './core/auth.guard';

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
                .then(m => m.DashboardComponent),
        canActivate: [authGuard]
    },
    {
        path: 'products',
        loadComponent: () =>
            import('./features/products/products.component')
                .then(m => m.ProductsComponent),
        canActivate: [authGuard]
    },
    {
        path: 'customers',
        loadComponent: () =>
            import('./features/customers/customers.component')
                .then(m => m.CustomersComponent),
        canActivate: [authGuard]
    },
    {
        path: 'users',
        loadChildren: () => import('./features/users/routes').then(m => m.default),
        canActivate: [authGuard]
    },
    {
        path: 'invoices',
        loadChildren: () => import('./features/invoices/routes').then(m => m.default),
        canActivate: [authGuard]
    },
    {
        path: 'configuration',
        loadComponent: () =>
            import('./features/configuration/configuration.component')
                .then(m => m.ConfigurationComponent),
        canActivate: [authGuard]
    },
    {
        path: '**',
        redirectTo: 'login'
    }
];

