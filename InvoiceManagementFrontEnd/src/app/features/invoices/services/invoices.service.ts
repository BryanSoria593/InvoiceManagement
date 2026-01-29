import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Invoice } from '../invoice.model';
import { environment } from '../../../../environments/environments';
import { PagedResult } from '../../../shared/models/paged-result.model';

@Injectable({ providedIn: 'root' })
export class InvoicesService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/invoices`;

    getPaged(pageNumber: number, pageSize: number, filter?: string): Observable<PagedResult<Invoice>> {
        const params: any = { pageNumber, pageSize };
        if (filter) {
            params.filter = filter;
        }
        return this.http.get<PagedResult<Invoice>>(`${this.apiUrl}`, { params });
    }

    create(payload: any): Observable<Invoice> {
        return this.http.post<Invoice>(`${this.apiUrl}`, payload);
    }

    getById(id: number): Observable<Invoice> {
        return this.http.get<Invoice>(`${this.apiUrl}/${id}`);
    }

    update(payload: any): Observable<Invoice> {
        return this.http.put<Invoice>(`${this.apiUrl}`, payload);
    }

    delete(id: number) {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
