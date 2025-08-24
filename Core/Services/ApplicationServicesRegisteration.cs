using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction;
using Services.MappingProfiles;

namespace Services
{
	public static class ApplicationServicesRegisteration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{

			services.AddAutoMapper(typeof(ProductProfile).Assembly);
			services.AddScoped<IServicesManager, ServicesManager>();
			return services;
		}
	}
}
