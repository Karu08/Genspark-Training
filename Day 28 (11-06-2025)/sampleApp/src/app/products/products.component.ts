import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ProductModel } from '../models/product';
import { Product } from "../product/product.component";
import { CartItem } from '../models/cartItem';



@Component({
  selector: 'app-products',
  standalone: true,
  //imports: [Product],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class Products implements OnInit {
  products:ProductModel[]|undefined=undefined;
  cartItems:CartItem[] =[];
  cartCount:number =0;
  constructor(private productService:ProductService){

  }
  handleAddToCart(event:Number)
  {
    console.log("Handling add to cart - "+event)
    let flag = false;
    for(let i=0;i<this.cartItems.length;i++)
    {
      if(this.cartItems[i].Id==event)
      {
         this.cartItems[i].Count++;
         flag=true;
      }
    }
    if(!flag)
      this.cartItems.push(new CartItem(event,1));
    this.cartCount++;
  }
  ngOnInit(): void {
    this.productService.getAllProducts().subscribe(
      {
        next:(data:any)=>{
         this.products = data.products as ProductModel[];
        },
        error:(err)=>{},
        complete:()=>{}
      }
    )
  }

}
