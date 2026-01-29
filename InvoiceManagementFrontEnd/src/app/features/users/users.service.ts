import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from './user.model';
import { environment } from '../../../environments/environments';
import { PagedResult } from '../../shared/models/paged-result.model';

@Injectable({ providedIn: 'root' })
export class UsersService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/users`;

  getPaged(pageNumber: number, pageSize: number, filter = ''): Observable<PagedResult<User>> {
    return this.http.get<PagedResult<User>>(`${this.apiUrl}/paged`, {
      params: { pageNumber, pageSize, filter }
    });
  }

  getById(id: number): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  create(user: Partial<User>): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, user);
  }

  update(user: User): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/update`, user);
  }

}
