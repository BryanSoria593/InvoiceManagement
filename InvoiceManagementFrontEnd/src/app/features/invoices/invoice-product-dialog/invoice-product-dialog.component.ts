import { Component, Inject, Input, Output, EventEmitter, inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { FormBuilder, FormGroup, FormArray, FormControl, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatDialogModule } from '@angular/material/dialog';
import { SearchFilterComponent } from '../../../shared/components/search-filter/search-filter.component';
import { ProductsService } from '../../products/products.service';
import { Subject, debounceTime } from 'rxjs';
import { AppProductService } from '../../../core/services/app-product.service';
import { Product } from '../../../core/models/product.model';

@Component({
    selector: 'app-invoice-product-dialog',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatButtonModule,
        MatIconModule,
        MatTableModule,
        MatPaginatorModule,
        MatDialogModule,
        SearchFilterComponent
    ],
    templateUrl: './invoice-product-dialog.component.html',
    styleUrls: ['./invoice-product-dialog.component.css']
})
export class InvoiceProductDialogComponent {
    displayedColumns = ['code', 'name', 'quantity', 'unitPrice', 'add'];
    dataSource = new MatTableDataSource<Product>([]);
    pageIndex = 0;
    pageSize = 5;
    total = 0;
    filter = '';
    pageSizeOptions = [5, 10, 20, 50];

    formArray = new FormArray<FormGroup<any>>([]);
    private productsService = inject(AppProductService);
    filter$ = new Subject<string>();

    constructor(
        public dialogRef: MatDialogRef<InvoiceProductDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: any
    ) {
        this.filter$
            .pipe(debounceTime(300))
            .subscribe(value => {
                this.filter = value;
                this.pageIndex = 0;
                this.loadProducts();
            });
        this.loadProducts();
    }

    applyFilter(value: string) {
        this.filter$.next(value);
    }

    initFormArray() {
        this.formArray.clear();
        this.dataSource.data.forEach(product => {
            this.formArray.push(new FormGroup({
                quantity: new FormControl(1),
                unitPrice: new FormControl(product.salePrice),
                total: new FormControl(product.salePrice),
                productId: new FormControl(product.id),
                productName: new FormControl(product.name),
                code: new FormControl(product.code)
            }));
        });
    }

    loadProducts() {
        this.productsService.getPaged(this.pageIndex + 1, this.pageSize, this.filter).subscribe(res => {
            this.dataSource.data = res.items;
            this.total = res.totalCount;
            this.initFormArray();
        });
    }

    // Métodos helper para obtener los controles
    getQuantityControl(index: number): FormControl {
        return this.formArray.at(index).get('quantity') as FormControl;
    }

    getUnitPriceControl(index: number): FormControl {
        return this.formArray.at(index).get('unitPrice') as FormControl;
    }

    onQuantityOrPriceChange(i: number) {
        const group = this.formArray.at(i) as FormGroup;
        const quantity = group.get('quantity')?.value || 0;
        const unitPrice = group.get('unitPrice')?.value || 0;
        group.get('total')?.setValue(quantity * unitPrice, { emitEvent: false });
    }

    addProduct(i: number) {
        const detail = this.formArray.at(i).value;
        this.dialogRef.close(detail);
    }

    onPage(event: PageEvent) {
        this.pageIndex = event.pageIndex;
        this.pageSize = event.pageSize;
        this.loadProducts();
    }

    close() {
        this.dialogRef.close();
    }
}