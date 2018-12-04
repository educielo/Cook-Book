using Repository.Pattern.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Models.Entities
{
    public class CheckList : Entity
    {
        public int Id { get; set; }
        public string IngredientName { get; set; }
        public int RecipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
