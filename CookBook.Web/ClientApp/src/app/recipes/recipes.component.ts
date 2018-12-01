import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-recipes',
  templateUrl: './recipes.component.html'
})
export class RecipesComponent {
  public forecasts: Recipes[];

  constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    http.get<Recipes[]>(baseUrl + 'api/SampleData/WeatherForecasts').subscribe(result => {
      this.forecasts = result;
    }, error => console.error(error));
  }
}

interface Recipes {
  dateFormatted: string;
  temperatureC: number;
  temperatureF: number;
  summary: string;
}
