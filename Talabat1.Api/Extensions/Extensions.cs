using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Identity;
using Services;
using Shared;
using Shared.ErrorModels;
using System.Text;
using Talabat1.Api.Middlewares;
namespace Talabat1.Api.Extensions
{
	public static class Extensions
	{
		public static IServiceCollection RegisterAllServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddBuiltInServices();

			services.AddSwaggerServices();

			services.AddInfrastructureServices(configuration);
			services.AddIdentityServices();
			services.AddApplicationServices(configuration);
			services.ConfigureService();
			services.ConfigureJwtService(configuration);
			return services;
		}
		public static async Task<WebApplication> ConfigureMiddlewares(this WebApplication app)
		{
			await app.InitializeDbAsync();
			app.UseMiddleware<GlobalErrorHandlingMiddleware>();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();

			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();
			return app;

		}
		private static IServiceCollection ConfigureJwtService(this IServiceCollection services,IConfiguration configuration)
		{
			var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

			services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateAudience = true,
					ValidateIssuer = true,
					ValidateIssuerSigningKey = true,
					ValidateLifetime = true,
					ValidIssuer = jwtOptions.Issuer,
					ValidAudience = jwtOptions.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
				};
			});
			return services;
		}
		private static IServiceCollection AddIdentityServices(this IServiceCollection services)
		{
			services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<TalabatIdentityDbContext>();
			return services;
		}
		private static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Allow clr to create object of type IDbInitializer 
			await dbInitializer.InitializeAsync();
			await dbInitializer.InitializeIdentityAsync();

			return app;
		}
		private static IServiceCollection AddBuiltInServices(this IServiceCollection services)
		{
			services.AddControllers();
			return services;
		}
		private static IServiceCollection AddSwaggerServices(this IServiceCollection services)
		{
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen(); 
			return services;
		}
		private static IServiceCollection ConfigureService(this IServiceCollection services)
		{
			services.Configure<ApiBehaviorOptions>(config =>
			{
				config.InvalidModelStateResponseFactory = (actionContext) =>
				{
					var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
					.Select(m => new ValidationError()
					{
						Field = m.Key,
						Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
					});
					var response = new ValidationErrorResponse()
					{
						Errors = errors
					};
					return new BadRequestObjectResult(response);
				};
			});

			return services;
		}
	}
}
