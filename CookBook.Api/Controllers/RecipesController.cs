using CookBook.Api.Models;
using CookBook.Models;
using CookBook.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]  
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;
        private readonly UserManager<ApplicationUser> _userManager;

        public RecipesController(IRecipeService recipeService, IUnitOfWorkAsync unitOfWorkAsync, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _recipeService = recipeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create([FromBody] RecipeViewModel recipe)
        {
            string userId = User.Claims.First()?.Value;
            var user = await _userManager.FindByNameAsync(userId);
            var newRecipe = new CookBook.Models.Entities.Recipe()
            {
                RecipeName = recipe.RecipeName,
                CreationDate = DateTime.Now,
                Description = recipe.Description,
                Creator = user?.FullName,
            };
            _recipeService.Insert(newRecipe);
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok(new {message="Succesfully added a recipe" });
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete(int recipeId)
        {
            var recipe = _recipeService.Find(recipeId);
            if (recipe == null)
            {
                return NotFound();
            }
            _recipeService.Delete(recipeId);
           
            await _unitOfWorkAsync.SaveChangesAsync();
            return Ok(new { message = "Succesfully deleted a recipe!" });
        }
        [HttpGet]
        [Route("[action]")]
        [Produces("application/json")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Edit(int recipeId)
        {
            string userId = User.Claims.First()?.Value;
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
                return NotFound();          
            var recipe = await _recipeService.FindAsync(recipeId);
            if (recipe == null)
                return NotFound();
            if (user.FullName != recipe.Creator)
                return BadRequest(new { message = "Not your recipe!" });
            var model = new RecipeViewModel
            {
                RecipeName = recipe.RecipeName,
                Id = recipe.Id,
                Description = recipe.Description,
                CreationDate = recipe.CreationDate.ToShortDateString()
            };
            return Ok(recipe);
        }
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAllRecipes()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var recipes =  _recipeService.Query().Select(a => new Recipe()
            {
                CreationDate = a.CreationDate.ToShortDateString(),
                Creator = a.Creator,
                Description = a.Description,
                Id = a.Id,
                Ingredients = a.Ingredients,
                RecipeName = a.RecipeName,
            });
            return Ok(recipes);
        }

        [HttpGet]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetMyRecipes()
        {
            string userId = User.Claims.First()?.Value;
            var user = await _userManager.FindByNameAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var recipes = _recipeService.Query(a=>a.Creator == user.FullName).Select(a => new Recipe()
            {
                CreationDate = a.CreationDate.ToString("mm/dd/yyyy"),
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
