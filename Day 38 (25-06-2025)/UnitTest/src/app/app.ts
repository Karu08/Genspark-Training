import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { RecipeList } from './recipe-list/recipe-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RecipeList],
  templateUrl: './app.html',
  styleUrls: ['./app.css']
})
export class App {
  title = 'UnitTest';
}
