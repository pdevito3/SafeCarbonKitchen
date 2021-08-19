namespace RecipeManager.Extensions.Services
{
    using AutoMapper;
    using FluentValidation.AspNetCore;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;
    using Sieve.Services;
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Reflection;

    public static class WebApiServiceExtension
    {
        public static void AddWebApiServices(this IServiceCollection services)
        {
            services.AddMediatR(typeof(Startup));
            services.AddScoped<SieveProcessor>();
            services.AddMvc()
                .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}