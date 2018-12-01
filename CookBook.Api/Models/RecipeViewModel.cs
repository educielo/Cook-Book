using CookBook.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Models
{
    public class RecipeViewModel
    {
        public int Id { get; set; }
        [Required]
        public string RecipeName { get; set; }
        public string CreationDate { get;set; }
        public string Description { get; set; }

    }

    public class Recipe
    {
        public int Id { get; set; }
        [Required]
        public string RecipeName { get; set; }
        public string CreationDate { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
    }
}
