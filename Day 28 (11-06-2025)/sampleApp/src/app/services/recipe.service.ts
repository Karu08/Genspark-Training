import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs';
import { of } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RecipeService {
  constructor(private http: HttpClient) {}

  getRecipes() {
    //return of([]);
    return this.http.get<any>('https://dummyjson.com/recipes')
        .pipe(
            map(res => res.recipes)
      );
  }
}
