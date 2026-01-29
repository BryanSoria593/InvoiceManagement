import { Component, ViewChild, signal, inject } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe } from '@angular/common';
import { Subject, debounceTime } from 'rxjs';

import { CustomersService } from './customers.service';
import { Customer, CustomerStatus } from './customer.model';
import { SearchFilterComponent } from '../../shared/components/search-filter/search-filter.component';
import { CreateOrEditCustomerComponent } from './create-or-edit-customer/create-or-edit-customer.component';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';
import { MenuLayoutComponent } from '../../core/components/menu-layout.component';

@Component({
    selector: 'app-customers',
    standalone: true,
    imports: [
        MatTableModule,
        MatPaginatorModule,
        MatIconModule,
        MatButtonModule,
        DatePipe,
        SearchFilterComponent,
        MenuLayoutComponent,
        MatDialogModule
    ],
    templateUrl: './customers.component.html',
    styleUrls: ['./customers.component.css']
})
export class CustomersComponent {
    private customersService = inject(CustomersService);
    private dialog = inject(MatDialog);

    displayedColumns = ['name', 'email', 'phone', 'status', 'createdAt', 'actions'];
    dataSource = new MatTableDataSource<Customer>([]);
    total = signal(0);
    pageIndex = signal(0);
    pageSize = signal(10);
    filter = signal('');
    pageSizeOptions = signal<number[]>([5, 10, 20, 50]);
    loading = signal(false);
    private filter$ = new Subject<string>();

    @ViewChild(MatPaginator) paginator!: MatPaginator;

    constructor() {
        this.filter$
            .pipe(debounceTime(300))
            .subscribe(value => {
                this.filter.set(value);
                this.pageIndex.set(0);
                this.loadCustomers();
            });
        this.loadCustomers();
    }

    applyFilter(value: string) {
        this.filter$.next(value);
    }

    onPage(event: PageEvent) {
        this.pageIndex.set(event.pageIndex);
        this.pageSize.set(event.pageSize);
        this.loadCustomers();
    }

    private loadCustomers() {
        this.loading.set(true);
        this.customersService
            .getPaged(
                this.pageIndex() + 1,
                this.pageSize(),
                this.filter()
            )
            .subscribe({
                next: res => {
                    this.dataSource.data = res.items;
                    this.total.set(res.totalCount);
                    this.loading.set(false);
                },
                error: err => {
                    console.error('Error al cargar clientes', err);
                    this.loading.set(false);
                }
            });
    }

    newCustomer(): void {
        const dialogRef = this.dialog.open(CreateOrEditCustomerComponent, {
            data: {
                customer: { name: '', email: '', phone: '', address: '', status: CustomerStatus.Active, createdAt: new Date().toISOString() },
                readOnly: false,
                title: 'Nuevo Cliente'
            },
            width: '500px',
            autoFocus: false
        });
        dialogRef.afterClosed().subscribe((created: Customer) => {
            if (created) {
                console.log(created);
                this.loading.set(true);
                this.customersService.create(created).subscribe({
                    next: () => {
                        this.loadCustomers();
                        this.loading.set(false);
                    },
                    error: () => this.loading.set(false)
                });
            }
        });
    }

    viewCustomer(customer: Customer): void {
        this.dialog.open(CreateOrEditCustomerComponent, {
            data: {
                customer: { ...customer },
                title: 'Ver Cliente',
                readOnly: true
            },
            width: '500px',
            autoFocus: false
        });
    }

    editCustomer(customer: Customer): void {
        const dialogRef = this.dialog.open(CreateOrEditCustomerComponent, {
            data: {
                customer: { ...customer },
                readOnly: false,
                title: 'Editar Cliente'
            },
            width: '500px',
            autoFocus: false
        });
        dialogRef.afterClosed().subscribe((updated: Customer) => {
            if (updated) {
                this.loading.set(true);
                this.customersService.update(updated).subscribe({
                    next: () => {
                        this.loadCustomers();
                        this.loading.set(false);
                    },
                    error: () => this.loading.set(false)
                });
            }
        });
    }

    deleteCustomer(customer: Customer) {
        this.dialog.open(ConfirmDialogComponent, {
            data: {
                entity: 'cliente',
                name: customer.name
            },
            width: '350px',
            autoFocus: false
        }).afterClosed().subscribe(result => {
            if (result === true) {
                this.loading.set(true);
                this.customersService
                    .delete(customer.id)
                    .subscribe({
                        next: () => {
                            this.loadCustomers();
                            this.loading.set(false);
                        },
                        error: () => this.loading.set(false)
                    });
            }
        });
    }

    getStatusLabel(status: CustomerStatus): string {
        return status === CustomerStatus.Active ? 'Activo' : 'Inactivo';
    }
}
