import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, retry } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Recipe } from '../_models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class RecipeService {
  constructor(private http: HttpClient) { }

  create(recipeName: string, description: string, id: number, creationDate: string) {
    return this.http.post<any>(`${environment.apiEndpoint}recipes/create`, { recipeName, description,id,creationDate })
      .pipe(map(recipe => {
        return recipe;
      }));
  }

  find(recipeId: number) {
    return this.http.get<Recipe>(`${environment.apiEndpoint}recipes/edit`, {
      params: {
        recipeId: recipeId.toString(),
      }
    });
  }

  delete(recipeId: number) {
    return this.http.get<any>(`${environment.apiEndpoint}recipes/delete`, {
      params: {
        recipeId: recipeId.toString(),
      }
    });
  }
  getRecipes(): Observable<Recipe[]> {
    return this.http.get<Recipe[]>(`${environment.apiEndpoint}recipes/getMyRecipes`);
  }
  getAllRecipes() {
    return this.http.get<Recipe[]>(`${environment.apiEndpoint}recipes/GetAllRecipes`);
  }

  getMyRecipes() {
    return this.http.get<Recipe[]>(`${environment.apiEndpoint}Recipes/GetMyRecipes`);
  }

  update(recipeName: string, description: string) {
    return this.http.post<any>(`${environment.apiEndpoint}recipes/update`, { recipeName, description })
      .pipe(map(recipe => {
        return recipe;
      }));
  }

}
