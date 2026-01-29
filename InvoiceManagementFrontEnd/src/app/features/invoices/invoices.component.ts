import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { InvoicesService } from './services/invoices.service';
import { Invoice } from './invoice.model';
import { MatIcon } from '@angular/material/icon';
import { MenuLayoutComponent } from '../../core/components/menu-layout.component';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { SearchFilterComponent } from '../../shared/components/search-filter/search-filter.component';
import { debounceTime, Subject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
    selector: 'app-invoices',
    standalone: true,
    imports: [
        CommonModule,
        MatTableModule,
        MatButtonModule,
        MatIcon,
        MenuLayoutComponent,
        MatPaginatorModule,
        SearchFilterComponent
    ],
    templateUrl: './invoices.component.html',
    styleUrls: ['./invoices.component.css']
})
export class InvoicesComponent implements OnInit {
    displayedColumns = ['id', 'date', 'customerName', 'userName', 'status', 'total', 'actions'];
    dataSource = new MatTableDataSource<Invoice>([]);
    total = signal(0);
    pageIndex = signal(0);
    pageSize = signal(10);
    filter = signal('');
    pageSizeOptions = signal<number[]>([5, 10, 20, 50]);
    private filter$ = new Subject<string>();
    private service = inject(InvoicesService);
    private router = inject(Router);

    constructor() {
        this.filter$
            .pipe(debounceTime(300))
            .subscribe(value => {
                this.filter.set(value);
                this.pageIndex.set(0);
                this.loadInvoices();
            });

        this.loadInvoices();
    }

    ngOnInit() {
        this.loadInvoices();
    }

    onPage(event: PageEvent) {
        this.pageIndex.set(event.pageIndex);
        this.pageSize.set(event.pageSize);
        this.loadInvoices();
    }

    applyFilter(value: string) {
        this.filter$.next(value);
    }

    loadInvoices() {
        this.service.getPaged(
            this.pageIndex() + 1,
            this.pageSize(),
            this.filter()
        ).subscribe((response: any) => {
            const items = response.items ?? response;
            this.dataSource.data = items.map((inv: any) => ({
                ...inv,
                status: this.mapStatus(inv.status)
            }));
            this.total.set(response.totalCount);
        });
    }

    mapStatus(status: number): string {
        switch (status) {
            case 41: return 'Pendiente';
            case 42: return 'Pagada';
            default: return '' + status;
        }
    }

    onCreateInvoice() {
        this.router.navigate(['/invoices/create']);
    }

    downloadInvoice(invoice: any): void {
        console.log('Descargando factura', invoice.id);
    }
}
