using CookBook.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Initializer
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("abc@xyz.com").Result == null)
            {
                ApplicationUser nicole = new ApplicationUser
                {
                    UserName = "NicoleReyes@cookbook.com",
                    FullName="Nicol Reyes",
                    Email = "NicoleReyes@cookbook.com"
                    
                };
                ApplicationUser paul = new ApplicationUser
                {
                    UserName = "PaulSantos@cookbook.com",
                    Email = "PaulSantos@cookbook.com",
                    FullName = "John Paul Santos"
                    
                };
                IdentityResult result2 = userManager.CreateAsync(nicole, "Nicole@123").Result;

                IdentityResult result = userManager.CreateAsync(paul, "Paul@123").Result;
                
            }
        }
    }
}
