import { Component } from '@angular/core';
import { UserService } from '../services/UserService';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [RouterLink],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.css'
})
export class MenuComponent {
  username$:any;
  usrname:string|null = "";

  constructor(private userService:UserService)
  {
    //this.username$ = this.userService.username$;
    this.userService.username$.subscribe(
      {
       next:(value) =>{
          this.usrname = value ;
        },
        error:(err)=>{
          alert(err);
        }
      }
    )
  }
}