using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.MappingProfiles;
using Shared;

namespace Services
{
	public static class ApplicationServicesRegisteration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration)
		{

			services.AddAutoMapper(typeof(ProductProfile).Assembly);
			services.AddScoped<IServicesManager, ServicesManager>();
			services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));

			return services;
		}
	}
}
