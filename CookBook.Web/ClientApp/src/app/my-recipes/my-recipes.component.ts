import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { strictEqual } from 'assert';
import { Recipe } from '../_models';
import { RecipeService } from '../_services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-myrecipes',
  templateUrl: './my-recipes.component.html'
})
export class MyRecipeComponent implements OnInit {
  public recipes: Recipe[];

  constructor(
    private recipeService: RecipeService,

  ) {}
  ngOnInit() {
    this.recipeService.getMyRecipes().subscribe(res => this.recipes = res)
  }
}
