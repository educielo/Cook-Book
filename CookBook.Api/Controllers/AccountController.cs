using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using CookBook.Api.Models;
using CookBook.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CookBook.Web.Controllers
{
    [Route("api/[controller]")]
    public partial class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Login([FromBody] Login model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);

            if (result.Succeeded)
            {
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Username);
                var user = new User()
                {
                    FullName = appUser.FullName,
                    Email = appUser.Email,
                    Token = GenerateJwtToken(model.Username, appUser).ToString()
                };
                return Ok(user);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<object> Register([FromBody] Register model)
        {
            var newUser = new ApplicationUser
            {
                UserName = model.Email,
                FullName = model.FullName,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(newUser, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false);
                var appUser = _userManager.Users.SingleOrDefault(r => r.Email == model.Email);
                var user = new User()
                {
                    FullName = appUser.FullName,
                    Email = appUser.Email,
                    Token = GenerateJwtToken(appUser.UserName, appUser).ToString()
                };
                return Ok(user);
            }
            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private object GenerateJwtToken(string email, IdentityUser user)
        {
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.Email, "Token"),
                new[]
                {
                    new Claim("ID",user.Id.ToString())
                }
                );
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));
            var keyByteArray = Encoding.ASCII.GetBytes(_configuration["JwtKey"]);
            var signingKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(keyByteArray);
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _configuration["JwtIssuer"],
                Audience = "Audience",
                SigningCredentials = new SigningCredentials(signingKey,SecurityAlgorithms.HmacSha256),
                Subject = identity,
                Expires = expires,
                NotBefore= DateTime.Now,
            });





            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return handler.WriteToken(securityToken);
        }
    }
}