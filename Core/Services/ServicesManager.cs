using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using Shared;

namespace Services
{
	public class ServicesManager(IUnitOfWork unitOfWork,
		IMapper mapper,
		IBasketRepository basketRepository,
		ICacheRepository cacheRepository,
		UserManager<AppUser> userManager,
		IOptions<JwtOptions> options) : IServicesManager
	{
		public IProductService ProductService { get; } = new ProductService(unitOfWork,mapper);

		public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

		public ICacheService CacheService { get; } = new CacheService(cacheRepository);

		public IAuthService AuthService { get; } = new AuthService(userManager,options);
	}
}
