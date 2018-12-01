using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Models
{
    public class IngredientViewModel
    {
        public int Id { get; set; }
        [Required]
        public string IngredientName { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
