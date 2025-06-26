import { Component, OnInit } from '@angular/core';
import { Recipe } from '../models/recipe';
import { CommonModule } from '@angular/common';
import { RecipeService } from '../services/recipe.service';
import { RecipeItem } from '../recipe-item/recipe-item';

@Component({
  selector: 'app-recipe-list',
  standalone: true,
  imports: [CommonModule, RecipeItem],
  templateUrl: './recipe-list.html',
  styleUrls: ['./recipe-list.css']
})
export class RecipeList implements OnInit {
  recipes: Recipe[] = [];
  loading = true;

  constructor(private recipeService: RecipeService) {}

  ngOnInit(): void {
    this.recipeService.getRecipes().subscribe({
      next: (res) => {
        this.recipes = res.recipes;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }
}
