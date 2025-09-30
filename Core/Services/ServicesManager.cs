using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
	public class ServicesManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository,ICacheRepository cacheRepository) : IServicesManager
	{
		public IProductService ProductService { get; } = new ProductService(unitOfWork,mapper);

		public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);

		public ICacheService CacheService { get; } = new CacheService(cacheRepository);
	}
}
