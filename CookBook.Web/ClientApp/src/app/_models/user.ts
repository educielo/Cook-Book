export class User {
    id: number;
    username: string;
    password: string;
    firstName: string;
    lastName: string;
}
export class Recipe {
  id: string;
  recipeName: string;
  creationDate: string;
  description: string;
  creator: string;
}

export class Ingredient {
  id: number;
  ingredientName: string;
  recipeId: number;
}
