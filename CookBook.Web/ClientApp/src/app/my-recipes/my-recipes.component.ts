import { Component, Inject, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { strictEqual } from 'assert';
import { Recipe } from '../_models';
import { RecipeService } from '../_services';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { error } from 'protractor';
@Component({
  selector: 'app-myrecipes',
  templateUrl: './my-recipes.component.html'
})
export class MyRecipeComponent implements OnInit {
  public recipes: Recipe[];
  message = '';
  constructor(
    private recipeService: RecipeService,

  ) {}
  ngOnInit() {
    this.recipeService.getMyRecipes().subscribe(res => { this.recipes = res }, error => console.log(error))
  }

  delete(passedId) {
    this.recipeService.delete(passedId).subscribe(result => {
      let indexOfId = this.recipes.findIndex((eachEleme) => {
        return eachEleme.id = passedId;
      });
      
      this.recipeService.getMyRecipes().subscribe(res => this.recipes = res)
      this.message = result.message;

    }, error => console.log('There was an error: ', error));
    setTimeout(() => {
      this.message = '';
    }, 3000)
  }
  viewRecipe(recipeId) {

  }
}
