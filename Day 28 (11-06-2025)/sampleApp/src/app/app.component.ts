import { Component } from '@angular/core';
import { RecipeListComponent } from './recipe-list/recipe-list.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RecipeListComponent],
  template: `<app-recipe-list></app-recipe-list>`,
  styleUrls: ['./app.component.css']
})
export class AppComponent {}
