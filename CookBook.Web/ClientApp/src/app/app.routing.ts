import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login';
import { AuthGuard } from './_guards';
import { MyRecipeComponent } from './my-recipes/my-recipes.component';
import { RecipeComponent } from './recipes/recipes.component';

const appRoutes: Routes = [
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginComponent },
    { path: 'my-recipes', component: MyRecipeComponent, canActivate: [AuthGuard] },
    { path: 'recipes', component: RecipeComponent, canActivate: [AuthGuard] },
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },

    // otherwise redirect to home
    { path: '**', redirectTo: '' }
];

export const routing = RouterModule.forRoot(appRoutes);
