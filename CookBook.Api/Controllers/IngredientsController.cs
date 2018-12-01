using CookBook.Api.Models;
using CookBook.Models.Entities;
using CookBook.Service;
using Microsoft.AspNetCore.Mvc;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers
{
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly IIngredientService _ingredientService;
        public IngredientsController(IUnitOfWorkAsync unitOfWorkAsync, IIngredientService ingredientService)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ingredientService = ingredientService;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] IngredientViewModel ingredient)
        {
            var newIngredient = new Ingredient()
            {
                IngredientName = ingredient.IngredientName,
                RecipeId = ingredient.RecipeId,
            };
            _ingredientService.Insert(newIngredient);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]       
        public async Task<IActionResult> Delete(int ingredientId)
        {
            var recipe = _ingredientService.Find(ingredientId);
            if (recipe == null)
            {
                return NotFound();
            }
            _ingredientService.Delete(ingredientId);

            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Edit(int ingredientId)
        {
            var recipe = await _ingredientService.FindAsync(ingredientId);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Update([FromBody] IngredientViewModel ingredient)
        {
            if (ingredient == null)
            {
                return NotFound();
            }
            var oldIngredient = await _ingredientService.FindAsync(ingredient.Id);
            if (oldIngredient == null)
            {
                return NotFound();
            }
            oldIngredient.IngredientName = ingredient.IngredientName;
            oldIngredient.RecipeId = ingredient.RecipeId;
            oldIngredient.ObjectState = Repository.Pattern.Infrastracture.ObjectState.Modified;
            _ingredientService.Update(oldIngredient);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetIngredients(int recipeId)
        {
            var ingredients=  _ingredientService.Query(a => a.RecipeId == recipeId).Select();
            return Ok(ingredients);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAllIngredients()
        {
            var ingredients = _ingredientService.Query().Include(a=>a.Recipe).Select(a=> new Ingredient() {
                Id = a.Id,
                IngredientName = a.IngredientName,
                RecipeId = a.RecipeId,
               
            });
            return Ok(ingredients);
        }
    }
}
