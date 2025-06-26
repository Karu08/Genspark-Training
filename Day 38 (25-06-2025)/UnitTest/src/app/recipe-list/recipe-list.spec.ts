import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecipeList } from './recipe-list';
import { of } from 'rxjs';
import { RecipeService } from '../services/recipe.service';
import { Recipe } from '../models/recipe';
import { RecipeItem} from '../recipe-item/recipe-item';
import { By } from '@angular/platform-browser';

describe('RecipeListComponent', () => {
  let component: RecipeList;
  let fixture: ComponentFixture<RecipeList>;

  beforeEach(() => {
    const mockRecipes: Recipe[] = [{
      id: 1,
      name: 'Pasta',
      ingredients: ['noodles', 'sauce'],
      instructions: [],
      prepTimeMinutes: 10,
      cuisine: 'Italian'
    }];

    const recipeServiceStub = {
      getRecipes: () => of({ recipes: mockRecipes })
    };

    TestBed.configureTestingModule({
      imports: [RecipeList, RecipeItem],
      providers: [{ provide: RecipeService, useValue: recipeServiceStub }]
    }).compileComponents();

    fixture = TestBed.createComponent(RecipeList);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should load and display recipes', () => {
    const items = fixture.debugElement.queryAll(By.directive(RecipeItem));
    expect(items.length).toBe(1);
  });
});
