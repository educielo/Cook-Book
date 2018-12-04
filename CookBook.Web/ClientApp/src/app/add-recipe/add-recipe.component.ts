import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { RecipeService } from '../_services';

@Component({templateUrl: 'add-recipe.component.html'})
export class AddRecipeComponent implements OnInit {
  addRecipeForm: FormGroup;
    loading = false;
    submitted = false;
  error = '';
  message = '';

    constructor(
        private formBuilder: FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
      private recipeService: RecipeService) {}

    ngOnInit() {
      this.addRecipeForm = this.formBuilder.group({
            name: ['', Validators.required],
            description: ['', Validators.required]
        });
    }

    // convenience getter for easy access to form fields
  get f() { return this.addRecipeForm.controls; }

    onSubmit() {
        this.submitted = true;

        // stop here if form is invalid
      if (this.addRecipeForm.invalid) {
            return;
        }

      this.loading = true;
      this.recipeService.create(this.f.name.value, this.f.description.value,0,null)
            .pipe(first())
            .subscribe(
                data => {
                  this.message = data.message;
                  this.loading = false;
                  this.addRecipeForm.reset();
                  for (var name in this.addRecipeForm.controls) {
                    this.addRecipeForm.controls[name].setErrors(null);
                  }
                },
          error => {
            console.log(error);
                    this.error = error;
                    this.loading = false;
        });
     
    }
}
