import { Component, OnInit } from '@angular/core';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { FormsModule } from '@angular/forms'; 
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './dashboard.html',
  styleUrls: ['./dashboard.css']
})

export class Dashboard implements OnInit {
  users: User[] = [];
  filteredUsers: User[] = [];
  genderFilter = '';
  stateFilter = '';

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.addUser();      
    this.getUsers();     
  }

  addUser() {
    const user: User = {
      firstName: 'Muhammad',
      lastName: 'Ovi',
      age: 30,
      gender: 'male',
      address: { state: 'California' },
      role: 'admin'
    };

    this.userService.addUser(user).subscribe(res => console.log('User added:', res));
  }

  getUsers() {
    this.userService.getAllUsers().subscribe(res => {
      this.users = res.users.map(u => ({
        ...u,
        role: Math.random() > 0.5 ? 'admin' : 'user' 
      }));
      this.filteredUsers = [...this.users];
    });
  }

  applyFilters() {
    this.filteredUsers = this.users.filter(user => {
      const matchGender = this.genderFilter ? user.gender === this.genderFilter : true;
      const matchState = this.stateFilter ? user.address?.state?.toLowerCase().includes(this.stateFilter.toLowerCase()) : true;
      return matchGender && matchState;
    });
  }

  resetFilters() {
    this.genderFilter = '';
    this.stateFilter = '';
    this.filteredUsers = [...this.users];
  }

  trackByName(index: number, user: User): string {
  return user.firstName + user.lastName;
}

}
