using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
