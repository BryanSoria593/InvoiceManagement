import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environments';
import { Observable } from 'rxjs';
import { User } from '../../shared/models/user.model';

@Injectable({ providedIn: 'root' })
export class AppUserService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/users`;

    get(): Observable<User[]> {
        return this.http.get<User[]>(`${this.apiUrl}`);
    }

}
