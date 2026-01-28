import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from './product.model';
import { environment } from '../../../environments/environments';
import { PagedResult } from '../../shared/models/paged-result.model';

@Injectable({ providedIn: 'root' })
export class ProductsService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/products`;

  constructor() {}


  getPaged(pageNumber: number, pageSize: number): Observable<PagedResult<Product>> {
    return this.http.get<PagedResult<Product>>(`${this.apiUrl}`, {
      params: { pageNumber, pageSize }
    });
  }

  getById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/${id}`);
  }

  create(product: Partial<Product>): Observable<Product> {
    return this.http.post<Product>(this.apiUrl, product);
  }

  update(product: Product): Observable<Product> {
    return this.http.put<Product>(this.apiUrl, product);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
