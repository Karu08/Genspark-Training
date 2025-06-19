import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { User } from '../models/user';
import { addUSer } from '../ngrx/users.actions';

@Component({
  selector: 'app-add-user',
  imports: [],
  templateUrl: './add-user.component.html',
  styleUrl: './add-user.component.css'
})
export class AddUserComponent {
    constructor(private store:Store) {
     
     }
     handelAddUser(){
       const newUSer = new User(102,"Doe","Doe","user");
      this.store.dispatch(addUSer({ user: newUSer }));
     }
}