using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
	public class ServicesManager(IUnitOfWork unitOfWork,IMapper mapper,IBasketRepository basketRepository) : IServicesManager
	{
		public IProductService ProductService { get; } = new ProductService(unitOfWork,mapper);

		public IBasketService BasketService { get; } = new BasketService(basketRepository, mapper);
	}
}
