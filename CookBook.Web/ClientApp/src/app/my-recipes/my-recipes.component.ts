import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { strictEqual } from 'assert';

@Component({
  selector: 'app-recipe',
  templateUrl: './my-recipes.component.html'
})
export class MyRecipeComponent {
  public recipes: Recipes[];

  constructor(http: HttpClient) {
    http.get<Recipes[]>('https://localhost:44328/api/Recipes/GetAllRecipes/').subscribe(result => {
      console.log(result);
      this.recipes = result;

    }, error => console.error(error));
  }
}

interface Recipes {
  id: string;
  recipeName: string;
  creationDate: string;
  description: string;
  creator: string;
}

interface Ingredient {
  id: number;
  ingredientName: string;
  recipeId: number;
}
