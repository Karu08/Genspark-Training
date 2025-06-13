import { Injectable } from '@angular/core';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private dummyUsers: User[] = [
    new User('ram', 'ram123'),
    new User('shyam', 'shyam123')
  ];

  getLoggedInUser(): User | null {
  if (typeof window !== 'undefined') {
    const data = localStorage.getItem('loggedInUser');
    return data ? JSON.parse(data) : null;
  }
  return null;
}

login(username: string, password: string): User | null {
  const user = this.dummyUsers.find(u => u.username === username && u.password === password);
  if (user && typeof window !== 'undefined') {
    localStorage.setItem('loggedInUser', JSON.stringify(user));
    return user;
  }
  return null;
}

logout() {
  if (typeof window !== 'undefined') {
    localStorage.removeItem('loggedInUser');
  }
}

}
