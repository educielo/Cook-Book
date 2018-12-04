import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, retry } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Recipe, Ingredient } from '../_models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class IngredientService {
  constructor(private http: HttpClient) { }

  create(ingredientName: string, description: string, id: number, recipeId: number) {
    return this.http.post<any>(`${environment.apiEndpoint}ingredients/create`, { ingredientName, description, id, recipeId })
      .pipe(map(ingredient => {
        return ingredient;
      }));
  }

  find(ingredientId: number) {
    this.http.get<Ingredient>(`${environment.apiEndpoint}ingredients/edit`, {
      params: {
        ingredinetId: ingredientId.toString(),
      }
    });
  }

  delete(ingredientId: number) {
    return this.http.get<Ingredient>(`${environment.apiEndpoint}ingredients/delete`, {
      params: {
        ingredientId: ingredientId.toString(),
      }
    });
  }

  getIngredientsByRecipe(recipeId: number) {
    return this.http.get<Ingredient[]>(`${environment.apiEndpoint}ingredients/GetIngredients`, {
      params: {
        recipeId: recipeId.toString(),
      }
    });
  }

  update(ingredientName: string, description: string, isChecked: boolean, id: number, recipeId: number) {
    return this.http.post<any>(`${environment.apiEndpoint}ingredients/update`, { ingredientName, description, isChecked })
      .pipe(map(ingredient => {
        return ingredient;
      }));
  }

  setStatus(isChecked: boolean, id: number) {
    //alert(id);
    //const params = new URLSearchParams();
    //params.set('id', id.toString());
    //params.set('isChecked', isChecked.toString());
    //return this.http.post(`${environment.apiEndpoint}ingredients/SetChecked`, params)
    //  .subscribe(
    //    result => {
    //      console.log(result);
    //    },
    //    error => {
    //      console.log('There was an error: ')
    //    }
    //  );
    //return this.http.post<any>(`${environment.apiEndpoint}ingredients/SetChecked`, {
    //  params: {
    //    id: id.toString(),
    //    isChecked: isChecked,
    //  }
    //});
    return this.http.get<any>(`${environment.apiEndpoint}ingredients/SetChecked`, {
      params: {
        id: id.toString(),
        isChecked: isChecked.toString(),
      }
    });

  }

}
