import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import { selectAllUsers, selectUserError, selectUserLoading } from '../ngrx/user.selector';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { AddUserComponent } from "../add-user/add-user.component";

@Component({
  selector: 'app-user-list',
  imports: [NgIf, NgFor, AsyncPipe, AddUserComponent],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent implements OnInit {

  users$:Observable<User[]> ;
  loading$:Observable<boolean>;
  error$:Observable<string | null>;

  constructor(private store:Store){
    this.users$ = this.store.select(selectAllUsers);
    this.loading$ = this.store.select(selectUserLoading);
    this.error$ = this.store.select(selectUserError);

  }
  ngOnInit(): void {
    this.store.dispatch({ type: '[Users] Load Users' });
  }

}