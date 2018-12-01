using CookBook.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Pattern.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Models
{
    public class CookBookContext :  DataContext
    {
        public CookBookContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
