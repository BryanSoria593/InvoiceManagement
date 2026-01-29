import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Configuration } from '../../core/models/configuration.model';
import { environment } from '../../../environments/environments';

@Injectable({ providedIn: 'root' })
export class ConfigurationService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = `${environment.baseUrl}/configuration`;

  get(): Observable<Configuration> {
    return this.http.get<Configuration>(`${this.apiUrl}/single`);
  }

  update(config: Partial<Configuration>): Observable<Configuration> {
    return this.http.put<Configuration>(this.apiUrl, config);
  }
}
