// import { Component } from '@angular/core';
// import { FormsModule } from '@angular/forms';
// import { AuthService } from './services/auth.service';
// import { User } from './models/user';
// import { Login } from './login/login';

// @Component({
//   selector: 'app-root',
//   standalone: true,                 
//   imports: [FormsModule, Login],          
//   templateUrl: './app.html',
//   styleUrls: ['./app.css']
// })
// export class App {
//   protected title = 'login-demo';
// }

import { Component } from '@angular/core';
import { Menu } from "./menu/menu";
import { Login } from "./login/login";

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [Menu, Login]
})
export class App {
  protected title = 'myApp';
}