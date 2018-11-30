using CookBook.Models;
using CookBook.Models.Entities;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Linq;

namespace CookBook.Service
{
    public interface IRecipeService : IService<Recipe>
    {
        IQueryable<Recipe> GetAllRecipes();
    }

    public class RecipeService : Service<Recipe>, IRecipeService
    {
        private readonly IRepositoryAsync<Recipe> _repository;

        public RecipeService(IRepositoryAsync<Recipe> repository) : base(repository)
        {
            _repository = repository;
        }
        public IQueryable<Recipe> GetAllRecipes()
        {
            throw new NotImplementedException();
        }
    }
}
