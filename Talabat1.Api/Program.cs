
using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Services;
using Services.Abstraction;
using Services.MappingProfiles;
using Shared.ErrorModels;
using Talabat1.Api.Extensions;
using Talabat1.Api.Middlewares;

namespace Talabat1.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterAllServices(builder.Configuration);

            var app = builder.Build();

            await app.ConfigureMiddlewares();

            app.Run();
        }
    }
}
