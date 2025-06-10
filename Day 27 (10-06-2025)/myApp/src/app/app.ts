import { Component } from '@angular/core';
import { Navbar } from "./navbar/navbar";
import { Products } from "./products/products";
import { CustomerDetails } from "./customer-details/customer-details";

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './app.html',
  styleUrl: './app.css',
  imports: [ Navbar, Products, CustomerDetails]

})
export class App {
  protected title = 'myApp';
  cartCount = 0;

  updateCart(count: number) {
    this.cartCount = count;
  }
}
