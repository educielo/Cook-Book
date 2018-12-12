using CookBook.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookBook.Api.Configurations
{
    public static class ApplicationDbInitializer
    {
        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser
                {
                    UserName = "NicoleReyes@cookbook.com",
                    FullName="Nicole Reyes",
                    Email = "NicoleReyes@cookbook.com"
                },
                 new ApplicationUser
                {
                    UserName = "PaulSantos@cookbook.com",
                    Email = "PaulSantos@cookbook.com",
                    FullName = "John Paul Santos"
                },
            };

            foreach (var user in users)
            {
                if (userManager.FindByEmailAsync(user.Email).Result == null)
                {
                    IdentityResult result2 = userManager.CreateAsync(user, "Pass@123").Result;
                }
            }
        }
    }
}
