using CookBook.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repository.Pattern.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]  
    public class RecipesController : ControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IUnitOfWorkAsync _unitOfWorkAsync;

        public RecipesController(IRecipeService recipeService, IUnitOfWorkAsync unitOfWorkAsync)
        {
            _recipeService = recipeService;
            _unitOfWorkAsync = unitOfWorkAsync;
        }

        public async Task<IActionResult> Create()
        {
            
            return Ok();
        }

        public async Task<IActionResult> Delete()
        {

            return Ok();
        }

        public async Task<IActionResult> Update()
        {

            return Ok();
        }

    }
}
