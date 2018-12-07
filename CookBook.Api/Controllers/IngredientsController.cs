using CookBook.Api.Models;
using CookBook.Models;
using CookBook.Models.Entities;
using CookBook.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
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
        private readonly IRecipeService _recipeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public IngredientsController(IUnitOfWorkAsync unitOfWorkAsync, IIngredientService ingredientService,
            IRecipeService recipeService, UserManager<ApplicationUser> userManager)
        {
            _unitOfWorkAsync = unitOfWorkAsync;
            _ingredientService = ingredientService;
            _recipeService = recipeService;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] IngredientViewModel ingredient)
        {
            var newIngredient = new Ingredient()
            {
                IngredientName = ingredient.IngredientName,
                RecipeId = ingredient.RecipeId,
                Description = ingredient.Description,

            };
            _ingredientService.Insert(newIngredient);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok(new { Message = "New ingredient added to your list" });
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int ingredientId)
        {
            var recipe = _ingredientService.Find(ingredientId);
            if (recipe == null)
            {
                return NotFound();
            }
            _ingredientService.Delete(ingredientId);

            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok(new { Message = "An ingredient has been deleted from your list!" });
        }

        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
            oldIngredient.IsChecked = ingredient.IsChecked;
            oldIngredient.ObjectState = Repository.Pattern.Infrastracture.ObjectState.Modified;
            _ingredientService.Update(oldIngredient);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok();
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> SetChecked(int id, bool isChecked)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var oldIngredient = await _ingredientService.FindAsync(id);
            if (oldIngredient == null)
            {
                return NotFound();
            }       
            oldIngredient.IsChecked = isChecked;
            oldIngredient.ObjectState = Repository.Pattern.Infrastracture.ObjectState.Modified;
            _ingredientService.Update(oldIngredient);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok(new {message="Ingredient marked as check!" });
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetIngredients(int recipeId)
        {
            string userId = User.Claims.First()?.Value;
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
                return NotFound();
            var recipe = await _recipeService.FindAsync(recipeId);
            if (recipe == null)
                return NotFound();
            if (user.FullName != recipe.Creator)
                return BadRequest(new { message="Not your recipe!"});
            var ingredients=  _ingredientService.Query(a => a.RecipeId == recipeId).Select(a=>  new IngredientViewModel() {
                Id = a.Id,
                Description = a.Description,
                IngredientName = a.IngredientName,
                IsChecked = a.IsChecked,
                RecipeId =a.RecipeId,
            });
            return Ok(ingredients);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
