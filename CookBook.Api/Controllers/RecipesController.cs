using CookBook.Api.Models;
using CookBook.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]  
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public RecipesController(IRecipeService recipeService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _recipeService = recipeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Create([FromBody] RecipeViewModel recipe)
        {
            var newRecipe = new CookBook.Models.Entities.Recipe()
            {
                RecipeName = recipe.RecipeName,
                CreationDate = DateTime.Now,
                Description = recipe.Description,
                Creator ="educielo0604@gmail.com",
            };
            _recipeService.Insert(newRecipe);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]   
        public async Task<IActionResult> Delete(int recipeId)
        {
            var recipe = _recipeService.Find(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            _recipeService.Delete(recipeId);
           
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Edit(int recipeId)
        {
            var recipe = await _recipeService.FindAsync(recipeId);
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
        public async Task<IActionResult> Update([FromBody] RecipeViewModel recipe)
        {
            if (recipe == null)
            {
                return NotFound();
            }
            var oldRecipe = await _recipeService.FindAsync(recipe.Id);
            if (oldRecipe == null)
            {
                return NotFound();
            }
            oldRecipe.Description = recipe.Description;
            oldRecipe.RecipeName = recipe.RecipeName;
            oldRecipe.ObjectState = Repository.Pattern.Infrastracture.ObjectState.Modified;
            _recipeService.Update(oldRecipe);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IActionResult GetAllRecipes()
        {
            var recipes =  _recipeService.Query().Select(a => new Recipe()
            {
                CreationDate = a.CreationDate.ToString(),
                Creator = a.Creator,
                Description = a.Description,
                Id = a.Id,
                Ingredients = a.Ingredients,
                RecipeName = a.RecipeName,
            });
            return Ok(recipes);
        }
    }
}
