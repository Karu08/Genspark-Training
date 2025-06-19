import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { UserForm } from './user-form/user-form';
import { UserList } from './user-list/user-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ReactiveFormsModule, UserForm, UserList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'UserManagement';
}
