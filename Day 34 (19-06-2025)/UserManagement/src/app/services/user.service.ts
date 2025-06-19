import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import {User} from '../models/user';

@Injectable({
    providedIn: 'root'
})

export class UserService {
    private dummyUsers : User[] = [
        {username: 'ram', email:'ram@gmail.com', password:'ram123', role:'User'},
        {username: 'shyam', email:'shyam@gmail.com', password:'shyam123', role:'Admin'}
    ];

    private usersSubject = new BehaviorSubject<User[]>(this.dummyUsers);
    users$: Observable<User[]> = this.usersSubject.asObservable();

    constructor() {}

    addUser(user: User) {
        const currentUsers = this.usersSubject.getValue();
        this.usersSubject.next([...currentUsers, user]);
    }

    getUsers(): User[] {
        return this.usersSubject.getValue();
    }
}