import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environments';
import { Observable } from 'rxjs';
import { Customer } from '../models/customer.model';

@Injectable({ providedIn: 'root' })
export class AppCustomersService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/customers`;

    get(): Observable<Customer[]> {
        return this.http.get<Customer[]>(`${this.apiUrl}/all`);
    }

}
