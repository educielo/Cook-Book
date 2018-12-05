import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { IngredientService, RecipeService } from '../_services';
import { Ingredient, Recipe } from '../_models';

@Component({ templateUrl: 'add-ingredients.component.html' })
export class AddIngredientsComponent implements OnInit, OnDestroy {
  public ingredient: Ingredient;
  public recipe = new Recipe();
  public ingredients: Ingredient[];
  public recipeId: number;
  private sub: any;
  ingredientForm: FormGroup;

  loading = false;
  submitted = false;
  addForm: boolean;
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
      this.recipeService.find(this.recipeId).subscribe(res => {
        if (res) {
          this.recipe = res;
        }
      });
    });



    this.ingredientService.getIngredientsByRecipe(this.recipeId).subscribe(res => this.ingredients = res);

    this.ingredientForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['', Validators.required]
    });

  }

  ngOnDestroy(): void {
    this.sub.unsubscribe();
  }

  get f() { return this.ingredientForm.controls; }

  showAdd() {
    this.addForm = true;
  }

  hideAdd() {
    this.addForm = false;
  }

  onSubmit() {
    this.submitted = true;
    if (this.ingredientForm.invalid) {
      return;
    }
    this.loading = true;
    this.ingredientService.create(this.f.name.value, this.f.description.value, 0, this.recipeId)
      .pipe(first())
      .subscribe(
        data => {
          this.message = data.message;
          this.loading = false;
          this.ingredientForm.reset();
          for (var name in this.ingredientForm.controls) {
            this.ingredientForm.controls[name].setErrors(null);
          }
          this.ingredientForm = this.formBuilder.group({
            name: ['', Validators.required],
            description: ['', Validators.required]
          });
          this.ingredientService.getIngredientsByRecipe(this.recipeId).subscribe(res => this.ingredients = res);
        },

        error => {
          console.log(error);
          this.error = error;
          this.loading = false;
        });
    this.ingredientService.getIngredientsByRecipe(this.recipeId).subscribe(res => this.ingredients = res);
  }

  changeState(event: any, id: number) {
    var isChecked = event.target.checked;
    this.ingredientService.setStatus(isChecked, id)
      .pipe(first())
      .subscribe(
        data => {
          this.ingredientService.getIngredientsByRecipe(this.recipeId).subscribe(res => this.ingredients = res);
        },
        error => {
          console.log(error);
        });
  }

  delete(passedId) {
    this.ingredientService.delete(passedId).subscribe(result => {
      this.ingredientService.getIngredientsByRecipe(this.recipeId).subscribe(res => this.ingredients = res);
    }, error => console.log('There was an error: ', error));
  }
}
