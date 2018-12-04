import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login';
import { AuthGuard } from './_guards';
import { MyRecipeComponent } from './my-recipes/my-recipes.component';
import { RecipeComponent } from './recipes/recipes.component';
import { AddRecipeComponent } from './add-recipe/add-recipe.component';
import { AddIngredientsComponent } from './add-ingrdients';
import { CookComponent } from './cook';

const appRoutes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'add-recipe', component: AddRecipeComponent, canActivate: [AuthGuard] },
  { path: 'my-recipes', component: MyRecipeComponent, canActivate: [AuthGuard] },
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'add-ingredients/:id', component: AddIngredientsComponent, canActivate: [AuthGuard] },
  { path: 'cook/:id', component: CookComponent, canActivate: [AuthGuard] },
    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
