using CookBook.Models;
using CookBook.Models.Entities;
using CookBook.Service;
using Microsoft.Extensions.DependencyInjection;
using Repository.Pattern.Core;
using Repository.Pattern.DataContext;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Configurations
{
    public static class ServicesConfig
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IDataContextAsync, CookBookContext>();
            services.AddScoped<IUnitOfWorkAsync, UnitOfWork>();
            //-- Repositories and Services--//
            services.AddTransient<IRepositoryAsync<Recipe>, Repository<Recipe>>();
            services.AddTransient<IRecipeService, RecipeService>();
            services.AddTransient<IRepositoryAsync<Ingredient>, Repository<Ingredient>>();
            services.AddTransient<IIngredientService, IngredientService>();
        }
    }
}
