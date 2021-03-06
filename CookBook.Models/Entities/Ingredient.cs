﻿using Repository.Pattern.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Models.Entities
{
    public class Ingredient : Entity
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public string Description { get; set; }
        public int RecipeId { get; set; }
        public bool IsChecked { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
