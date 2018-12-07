export class User {
    id: number;
    username: string;
    password: string;
    fullName: string;
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
  description: string;
  recipeId: number;
  isChecked: boolean;
}
