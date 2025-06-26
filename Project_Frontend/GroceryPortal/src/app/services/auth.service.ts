import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {jwtDecode} from 'jwt-decode';

@Injectable({providedIn: 'root'})
export class AuthService {
    private apiUrl = 'http://localhost:5263/api/User';

    decodeToken(token: string): any {
  try {
    return jwtDecode(token);
  } catch (error) {
    console.error('Invalid JWT token:', error);
    return null;
  }
}
    constructor(private http: HttpClient) {}

    login(username: string, password: string): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/login`, { username, password });
    }

    register(data: {name: string; username: string; password: string; role: string; email: string; phoneNum: string}): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/register`, data);
    }

    storeToken(token : string) {
        localStorage.setItem('token', token);
    }

    getToken(): string|null {
        return localStorage.getItem('token');
    }

    getRole(): string {
    const token = this.getToken();
    if (!token) return '';
    const decoded: any = jwtDecode(token);
    return decoded.role || '';
  }

  getUsername(): string {
  const token = this.getToken();
  if (!token) return '';
  const decoded: any = this.decodeToken(token);
  return decoded.unique_name || '';
}


    logout() {
        localStorage.removeItem('token');
    }

    isLoggedIn(): boolean {
        return !!this.getToken();
    }
}