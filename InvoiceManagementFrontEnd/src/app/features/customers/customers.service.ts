import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Customer } from './customer.model';
import { environment } from '../../../environments/environments';
import { PagedResult } from '../../shared/models/paged-result.model';

@Injectable({ providedIn: 'root' })
export class CustomersService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/customers`;

  getPaged(pageNumber: number, pageSize: number, filter = ''): Observable<PagedResult<Customer>> {
    return this.http.get<PagedResult<Customer>>(this.apiUrl, {
      params: { pageNumber, pageSize, filter }
    });
  }

  getById(id: number): Observable<Customer> {
    return this.http.get<Customer>(`${this.apiUrl}/${id}`);
  }

  create(customer: Partial<Customer>): Observable<Customer> {
    return this.http.post<Customer>(this.apiUrl, customer);
  }

  update(customer: Customer): Observable<Customer> {
    return this.http.put<Customer>(this.apiUrl, customer);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
