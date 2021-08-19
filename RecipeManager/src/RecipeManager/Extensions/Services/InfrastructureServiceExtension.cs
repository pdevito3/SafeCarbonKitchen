namespace RecipeManager.Extensions.Services
{
    using RecipeManager.Databases;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            // DbContext -- Do Not Delete
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<RecipeManagerDbcontext>(options =>
                    options.UseInMemoryDatabase($"RecipeManager"));
            }
            else
            {
                services.AddDbContext<RecipeManagerDbcontext>(options =>
                    options.UseNpgsql(
                        configuration.GetConnectionString("RecipeManager"),
                        builder => builder.MigrationsAssembly(typeof(RecipeManagerDbcontext).Assembly.FullName)));
            }

            // Auth -- Do Not Delete
            if(env.EnvironmentName != "FunctionalTesting")
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.Authority = configuration["JwtSettings:Authority"];
                        options.Audience = configuration["JwtSettings:Audience"];
                    });
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("CanReadRecipes",
                    policy => policy.RequireClaim("scope", "recipes.read"));
                options.AddPolicy("CanAddRecipes",
                    policy => policy.RequireClaim("scope", "recipes.add"));
                options.AddPolicy("CanDeleteRecipes",
                    policy => policy.RequireClaim("scope", "recipes.delete"));
                options.AddPolicy("CanUpdateRecipes",
                    policy => policy.RequireClaim("scope", "recipes.update"));
            });
        }
    }
}
