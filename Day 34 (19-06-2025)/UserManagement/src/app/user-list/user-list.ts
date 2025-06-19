import { Component, OnInit, AfterViewInit, ViewChild, ElementRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';

import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { combineLatest, fromEvent, map, debounceTime, distinctUntilChanged, startWith, Observable } from 'rxjs';

@Component({
  standalone: true,
  selector: 'app-user-list',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './user-list.html',
  styleUrls: ['./user-list.css']
})
export class UserList implements OnInit, AfterViewInit {
  users$!: Observable<User[]>;
  filteredUsers$!: Observable<User[]>;

  @ViewChild('searchInput', { static: true }) searchInput!: ElementRef<HTMLInputElement>;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.users$ = this.userService.users$;
  }

  
ngAfterViewInit(): void {
  const search$ = fromEvent(this.searchInput.nativeElement, 'input').pipe(
    map((event: Event) => (event.target as HTMLInputElement).value.toLowerCase()),
    startWith(''), 
    debounceTime(300),
    distinctUntilChanged()
  );

  this.filteredUsers$ = combineLatest([this.users$, search$]).pipe(
    map(([users, searchTerm]) =>
      users.filter(user =>
        user.username.toLowerCase().includes(searchTerm) ||
        user.role.toLowerCase().includes(searchTerm)
      )
    )
  );
  }
}
