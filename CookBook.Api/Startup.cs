using System.IdentityModel.Tokens.Jwt;
using CookBook.Api.Configurations;
using CookBook.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CookBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //-- DbContexts--//
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);

            services.AddDbContext<CookBookContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Scoped);                     
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            services.AddHttpContextAccessor();

            //-- Services and Repositories--//                    
            services.ConfigureServices();

            //--Configure CORS on the CorsConfig
            services.ConfigureCors();        
            
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.ConfigureJwt(Configuration["JwtKey"], Configuration["JwtIssuer"]);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //== Configure other Middlewares
            services.ConfigureMiddlewares();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            if (env.IsDevelopment())
            {
                dbContext.Database.EnsureCreated();
                ApplicationDbInitializer.SeedUsers(userManager);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.ConfigureCustomErrorHandler();
            app.UseHttpsRedirection();
            app.ConfigureAppMiddlewares();
            app.ConfigureCors();
            app.UseAuthentication();
            app.UseMvc();

        }
    }
}
