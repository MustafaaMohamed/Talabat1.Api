using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using Services;
using Services.Abstraction;
using StackExchange.Redis;

namespace Persistence
{
	public static class InfrastructureServicesRegisteration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<TalabatDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});
			services.AddDbContext<TalabatIdentityDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
			});
			services.AddScoped<IDbInitializer, DbInitializer>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IBasketRepository, BasketRepository>();
			services.AddScoped<ICacheRepository,CacheRepository>();

			services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
			{
				return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
			});
			return services;
		}
	}
}
