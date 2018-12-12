using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NJsonSchema;
using NSwag.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;

namespace CookBook.Api.Configurations
{
    public static class MiddlewareConfigs
    {
        public static void ConfigureMiddlewares(this IServiceCollection services)
        {
            services.AddSwagger();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "CookBook Api",
                    Description = "Cooking some .Net Recipes Since 2013",
                    Contact = new Contact
                    {
                        Name = "This Guy",
                        Email = "educielo0604@gmail.com",
                        Url = ""
                    }
                });
            });
        }
        public static void ConfigureAppMiddlewares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUi3WithApiExplorer(settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling =
                    PropertyNameHandling.CamelCase;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CookBook Api");
            });
        }
    }
}
