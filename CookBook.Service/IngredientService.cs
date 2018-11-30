using CookBook.Models;
using CookBook.Models.Entities;
using Repository.Pattern.Repositories;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Service
{
    public interface IIngredientService:IService<Ingredient>
    {
        IQueryable<Ingredient> GetAll();
        IQueryable<Ingredient> GetByRecipeId(int recipeId);
    }

    public class IngredientService : Service<Ingredient>, IIngredientService
    {
        private readonly IRepositoryAsync<Ingredient> _repository;
        public IngredientService(IRepositoryAsync<Ingredient> repository) : base(repository)
        {
            _repository = repository;
        }

        public IQueryable<Ingredient> GetByRecipeId(int recipeId)
        {
            throw new NotImplementedException();
        }

        IQueryable<Ingredient> IIngredientService.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
