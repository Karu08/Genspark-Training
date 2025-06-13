import { Component } from '@angular/core';
import { RecipeListComponent } from './recipe-list/recipe-list.component';
import { Product } from './product/product.component';
import { Products } from './products/products.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [Product, Products],
  //template: `<app-recipe-list></app-recipe-list>`,
  template: `<app-product></app-product>`,
  styleUrls: ['./app.component.css']
})
export class AppComponent {}
