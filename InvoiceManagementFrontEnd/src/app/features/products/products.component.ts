import { Component, ViewChild, signal, inject } from '@angular/core';
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
    SearchFilterComponent
  ],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent {

  private productsService = inject(ProductsService);

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
        error: err => console.error('Error al cargar productos', err)
      });
  }

  deleteProduct(product: Product) {
    if (!confirm('¿Seguro que deseas eliminar este producto?')) return;

    this.productsService
      .delete(product.id)
      .subscribe(() => this.loadProducts());
  }

  editProduct(product: Product): void {
  }

  viewProduct(product: Product): void {
  }

  getStatusLabel(status: ProductStatus): string {
    return status === ProductStatus.Active ? 'Activo' : 'Inactivo';
  }
}
