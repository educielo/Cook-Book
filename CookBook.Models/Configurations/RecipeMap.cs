using CookBook.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Models.Configurations
{
    public class RecipeMap : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes")
                .HasKey(a => a.Id);

            builder.HasMany(t => t.Ingredients)
                .WithOne(t => t.Recipe)
                .HasForeignKey(a => a.RecipeId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
