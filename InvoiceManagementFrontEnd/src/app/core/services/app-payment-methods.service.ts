import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environments';
import { Observable } from 'rxjs';
import { PaymentMethod } from '../models/payment-method.model';

@Injectable({ providedIn: 'root' })
export class AppPaymentMethodsService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/paymentMethods`;

    get(): Observable<PaymentMethod[]> {
        return this.http.get<PaymentMethod[]>(`${this.apiUrl}`);
    }

}
