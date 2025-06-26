import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-home',
  imports: [],
  templateUrl: './home.html',
  styleUrl: './home.css'
})

export class Home {
  username = '';

  constructor(private auth: AuthService) {
    this.username = this.auth.getUsername();
  }
}
