namespace RecipeManager.Extensions.Application
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.Configuration;
    using RecipeManager.Middleware;

    public static class SwaggerAppExtension
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                config.OAuthClientId(configuration["JwtSettings:ClientId"]);
                config.OAuthUsePkce();
            });
        }
    }
}