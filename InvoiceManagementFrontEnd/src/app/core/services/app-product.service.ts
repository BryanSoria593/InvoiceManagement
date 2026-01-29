import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environments';
import { Observable } from 'rxjs';
import { PagedResult } from '../../shared/models/paged-result.model';
import { Product } from '../models/product.model';

@Injectable({ providedIn: 'root' })
export class AppProductService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/products`;

    getPaged(pageNumber: number, pageSize: number, filter?: string): Observable<PagedResult<Product>> {
        const params: any = { pageNumber, pageSize };
        if (filter) {
            params.filter = filter;
        }
        return this.http.get<PagedResult<Product>>(`${this.apiUrl}`, { params });
    }

}
