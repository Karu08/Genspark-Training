import { Component, signal, effect, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RecipeService } from '../services/recipe.service';

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {
  recipes = signal<any[]>([]);

  constructor(private recipeService: RecipeService) {}

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe(data => {
      console.log('Fetched recipes:', data); 
      this.recipes.set(data);
    });
  }
}
