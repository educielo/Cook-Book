import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { IngredientService, RecipeService } from '../_services';
import { Ingredient, Recipe } from '../_models';

@Component({ templateUrl: 'cook.component.html' })
export class CookComponent implements OnInit, OnDestroy {
  public ingredient: Ingredient;
  public recipe: Recipe;
  public ingredients: Ingredient[];
  public recipeId: number;
  private sub: any;
  loading = false;
  submitted = false;
  addForm = false;
  error = '';
  message = '';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private ingredientService: IngredientService,
    private recipeService: RecipeService) { }

  ngOnInit() {

    this.sub = this.route.params.subscribe(params => {
      this.recipeId = +params['id'];
    });

    this.recipeService.find(this.recipeId).subscribe(res => {
      this.recipe = res;
    });
    this.ingredientService.getIngredientsByRecipe(this.recipeId).subscribe(res => this.ingredients = res);
 
    
  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }
  

  showAdd() {
    this.addForm = true;
  }

  changeState(event: any, id: number) {
    
  }

}
