import { Component, OnInit, ViewChild } from '@angular/core';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatPaginator, MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { ProductsService } from './products.service';
import { Product, ProductStatus } from './product.model';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { MenuLayoutComponent } from '../../core/components/menu-layout.component';

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
    MenuLayoutComponent
  ],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit {
  displayedColumns: string[] = ['code', 'name', 'status', 'createdAt', 'salePrice', 'actions'];
  dataSource = new MatTableDataSource<Product>([]);
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  pageSize = 10;
  pageSizeOptions: number[] = [5, 10, 20, 50];
  pageIndex = 0;
  total = 0;

  constructor(private productsService: ProductsService) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(event?: PageEvent): void {
    const pageIndex = event ? event.pageIndex : this.pageIndex;
    const pageSize = event ? event.pageSize : this.pageSize;
    this.productsService.getPaged(pageIndex + 1, pageSize).subscribe({
      next: (result) => {
        this.dataSource.data = result.items;
        this.total = result.totalCount;
      },
      error: (err) => {
        console.error('Error al cargar productos', err);
      }
    });
  }

  editProduct(product: Product): void {
  }

  deleteProduct(product: Product): void {
    if (confirm('¿Seguro que deseas eliminar este producto?')) {
      this.productsService.delete(product.id).subscribe(() => this.loadProducts());
    }
  }

  viewProduct(product: Product): void {
  }

  getStatusLabel(status: ProductStatus): string {
    return status === ProductStatus.Active ? 'Activo' : 'Inactivo';
  }

  onPage(event: PageEvent) {
    this.pageIndex = event.pageIndex;
    this.pageSize = event.pageSize;
    this.loadProducts(event);
  }
}
