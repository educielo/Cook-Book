import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { strictEqual } from 'assert';
import { Recipe } from '../_models';
import { RecipeService } from '../_services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
@Component({
  selector: 'app-recipe',
  templateUrl: './recipes.component.html'
})
export class RecipeComponent implements OnInit {
  public recipes: Recipe[];

  constructor(
    private recipeService: RecipeService,

  ){}

  ngOnInit() {
    this.recipeService.getAllRecipes().subscribe(res => this.recipes = res)
  }
}

