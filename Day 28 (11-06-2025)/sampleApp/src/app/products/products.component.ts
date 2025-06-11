import { Component, OnInit } from '@angular/core';
import { ProductService } from '../services/product.service';
import { ProductModel } from '../models/product';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})

export class Products implements OnInit {
  products:ProductModel[]|undefined=undefined;
  constructor(private productService:ProductService){

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
