import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RecipeService } from './recipe.service';
import { Recipe } from '../models/recipe';

describe('RecipeService', () => {
  let service: RecipeService;
  let http: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [RecipeService]
    });
    service = TestBed.inject(RecipeService);
    http = TestBed.inject(HttpTestingController);
  });

  it('should fetch recipes', () => {
    const mockResponse = {
      recipes: [{ id: 1, name: 'Pizza', ingredients: [], instructions: [], prepTimeMinutes: 10, cookTimeMinutes: 20, servings: 2, cuisine: 'Italian' }]
    };

    service.getRecipes().subscribe(res => {
      expect(res.recipes.length).toBe(1);
      expect(res.recipes[0].name).toBe('Pizza');
    });

    const req = http.expectOne('https://dummyjson.com/recipes');
    expect(req.request.method).toBe('GET');
    req.flush(mockResponse);
  });
});
