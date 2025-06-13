
// import { Component } from '@angular/core';
// import { AuthService } from '../services/auth.service';
// import { User } from '../models/user';
// import { FormsModule } from '@angular/forms'; 
// import { NgIf } from '@angular/common'; 


// @Component({
//   selector: 'app-login',
//   standalone: true,                
//   imports: [FormsModule, NgIf],         
//   templateUrl: './login.html',    
//   styleUrls: ['./login.css']      
// })

// export class Login {
//   username = '';
//   password = '';
//   errorMessage = '';
//   user: User | null = null;

//   constructor(private authService: AuthService) {
//     this.user = this.authService.getLoggedInUser();
//   }

//   login() {
//     const result = this.authService.login(this.username, this.password);
//     if (result) {
//       this.user = result;
//     } else {
//       this.errorMessage = 'Invalid credentials';
//     }
//   }

//   logout() {
//     this.authService.logout();
//     this.user = null;
//     this.username = '';
//     this.password = '';
//   }
// }

import { Component } from '@angular/core';
import { UserLoginModel } from '../models/UserLoginModel';
import { UserService } from '../services/UserService';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
  providers: [UserService]
})
export class Login {
user:UserLoginModel = new UserLoginModel();
constructor(private userService:UserService){

}
handleLogin(){
  this.userService.validateUserLogin(this.user);
}
}