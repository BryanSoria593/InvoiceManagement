import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginRequest } from './login/login-request.model';
import { LoginResponse } from './login/login-response.model';
import { environment } from '../../../environments/environments';
import { RegisterRequest } from './register/register-request.model';
import { User } from '../../shared/models/user.model';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = environment.baseUrl;

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(
      `${this.baseUrl}/auth/login`,
      request
    );
  }

  register(request: RegisterRequest): Observable<User> {
    return this.http.post<User>(
      `${this.baseUrl}/users/register`,
      request
    );
  }
}
