import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({providedIn: 'root'})
export class AuthService {
    private apiUrl = 'http://localhost:5263/api/User';

    constructor(private http: HttpClient) {}

    login(username: string, password: string): Observable<any> {
        return this.http.post('${this.apiUrl}/login', {username, password});
    }

    register(data: {username: string; password: string; role: string}): Observable<any> {
        return this.http.post('${this.apiUrl}/register', data)
    }

    storeToken(token : string) {
        localStorage.setItem('token', token);
    }

    getToken(): string|null {
        return localStorage.getItem('token');
    }

    getRole(): string {
        const token = this.getToken();
        if(!token) return '';

        const payload = JSON.parse(atob(token.split('.')[1]));
        return payload.role;
    }

    logout() {
        localStorage.removeItem('token');
    }

    isLoggedIn(): boolean {
        return !!this.getToken();
    }
}