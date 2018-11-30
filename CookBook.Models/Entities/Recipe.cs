using Repository.Pattern.Core;
using System;
using System.Collections.Generic;

namespace CookBook.Models.Entities
{
    public class Recipe : Entity
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
        }
        public int Id { get; set; }
        public string RecipeName { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }     
        //public Guid UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        //public virtual ApplicationUser UserLinked { get; set; }
    }
}
