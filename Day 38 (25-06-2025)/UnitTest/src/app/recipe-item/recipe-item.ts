import { Component, Input } from '@angular/core';
import { Recipe } from '../models/recipe';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-recipe-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './recipe-item.html',
  styleUrls: ['./recipe-item.css']
})
export class RecipeItem {
  @Input() recipe!: Recipe;
}
