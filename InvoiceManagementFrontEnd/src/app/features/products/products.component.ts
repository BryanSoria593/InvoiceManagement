import { Component, ViewChild, signal, inject } from '@angular/core';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { Subject, debounceTime } from 'rxjs';

import { ProductsService } from './products.service';
import { Product, ProductStatus } from './product.model';
import { MenuLayoutComponent } from '../../core/components/menu-layout.component';
import { SearchFilterComponent } from '../../shared/components/search-filter/search-filter.component';
import { EditProductComponent } from './create-or-edit-product/create-or-edit-product.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ConfirmDialogComponent } from '../../shared/components/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [
    MatTableModule,
    MatPaginatorModule,
    MatIconModule,
    MatButtonModule,
    DatePipe,
    CurrencyPipe,
    MenuLayoutComponent,
    SearchFilterComponent,
    MatDialogModule,
    MatProgressSpinnerModule,
    ConfirmDialogComponent,
  ],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {
  private productsService = inject(ProductsService);
  private dialog = inject(MatDialog);

  displayedColumns = ['code', 'name', 'status', 'createdAt', 'salePrice', 'actions'];

  dataSource = new MatTableDataSource<Product>([]);

  total = signal(0);
  pageIndex = signal(0);
  pageSize = signal(10);
  filter = signal('');
  pageSizeOptions = signal<number[]>([5, 10, 20, 50]);

  private filter$ = new Subject<string>();

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor() {
    this.filter$
      .pipe(debounceTime(300))
      .subscribe(value => {
        this.filter.set(value);
        this.pageIndex.set(0);
        this.loadProducts();
      });

    this.loadProducts();
  }

  applyFilter(value: string) {
    this.filter$.next(value);
  }

  onPage(event: PageEvent) {
    this.pageIndex.set(event.pageIndex);
    this.pageSize.set(event.pageSize);
    this.loadProducts();
  }

  private loadProducts() {
    this.productsService
      .getPaged(
        this.pageIndex() + 1,
        this.pageSize(),
        this.filter()
      )
      .subscribe({
        next: res => {
          this.dataSource.data = res.items;
          this.total.set(res.totalCount);
        },
        error: err => {
          console.error('Error al cargar productos', err);
        }
      });
  }

  deleteProduct(product: Product) {
    this.dialog.open(ConfirmDialogComponent, {
      data: {
        entity: 'producto',
        name: product.name
      },
      width: '350px',
      autoFocus: false
    }).afterClosed().subscribe(result => {
      if (result === true) {
        this.productsService
          .delete(product.id)
          .subscribe({
            next: () => {
              this.loadProducts();
            },
          });
      }
    });
  }

  newProduct(): void {
    const dialogRef = this.dialog.open(EditProductComponent, {
      data: {
        product: { code: '', name: '', description: '', salePrice: 0, status: ProductStatus.Active, createdAt: new Date().toISOString() },
        readOnly: false,
        title: 'Nuevo Producto'
      },
      width: '500px',
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe((created: Product) => {
      if (created) {
        this.productsService.create(created).subscribe({
          next: () => {
            this.loadProducts();
          },
        });
      }
    });
  }

  editProduct(product: Product): void {
    const dialogRef = this.dialog.open(EditProductComponent, {
      data: {
        product: { ...product },
        readOnly: false,
        title: 'Editar Producto'
      },
      width: '500px',
      autoFocus: false
    });
    dialogRef.afterClosed().subscribe((updated: Product) => {
      if (updated) {
        this.productsService.update(updated).subscribe({
          next: () => {
            this.loadProducts();
          },
        });
      }
    });
  }

  viewProduct(product: Product): void {
    this.dialog.open(EditProductComponent, {
      data: {
        product: { ...product },
        readOnly: true,
        title: 'Ver Producto'
      },
      width: '500px',
      autoFocus: false
    });
  }

  getStatusLabel(status: ProductStatus): string {
    return status === ProductStatus.Active ? 'Activo' : 'Inactivo';
  }
}
