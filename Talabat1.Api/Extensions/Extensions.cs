using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Shared.ErrorModels;
using Talabat1.Api.Middlewares;

namespace Talabat1.Api.Extensions
{
	public static class Extensions
	{
		public static IServiceCollection RegisterAllServices(this IServiceCollection services,IConfiguration configuration)
		{

			services.AddBuiltInServices();

			services.AddSwaggerServices();

			services.AddInfrastructureServices(configuration);
			services.AddApplicationServices();
			services.ConfigureService();
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

			app.UseAuthorization();


			app.MapControllers();
			return app;

		}
		private static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
		{
			using var scope = app.Services.CreateScope();
			var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>(); // Allow clr to create object of type IDbInitializer 
			await dbInitializer.InitializeAsync();

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
