import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RecipeItem } from './recipe-item';
import { Recipe } from '../models/recipe';
import { Component } from '@angular/core';

describe('RecipeItemComponent', () => {
  let component: RecipeItem;
  let fixture: ComponentFixture<RecipeItem>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [RecipeItem]
    }).compileComponents();

    fixture = TestBed.createComponent(RecipeItem);
    component = fixture.componentInstance;

    const recipe: Recipe = {
      id: 1,
      name: 'Burger',
      ingredients: ['bun', 'patty'],
      instructions: [],
      prepTimeMinutes: 5,
      cuisine: 'American'
    };

    component.recipe = recipe;
    fixture.detectChanges();
  });

  it('should display recipe name', () => {
    const compiled = fixture.nativeElement;
    expect(compiled.querySelector('h3').textContent).toContain('Burger');
  });
});
