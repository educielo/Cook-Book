using CookBook.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CookBook.Models.Configurations
{
    public class IngredientsMap : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {

            builder.ToTable("Ingredients")
                .HasKey(a => a.Id);

            builder.HasOne(a => a.Recipe)
                .WithMany(a => a.Ingredients)
                .HasForeignKey(a => a.RecipeId);
        }
    }
}
