using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Repositories;

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
			services.AddScoped<IDbInitializer, DbInitializer>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();

			return services;
		}
	}
}
