import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environments';
import { Observable } from 'rxjs';
import { Configuration } from '../models/configuration.model';

@Injectable({ providedIn: 'root' })
export class AppConfigurationService {
    private readonly http = inject(HttpClient);
    private readonly apiUrl = `${environment.baseUrl}/configuration`;

    get(): Observable<Configuration> {
        return this.http.get<Configuration>(`${this.apiUrl}/single`);
    }

}
