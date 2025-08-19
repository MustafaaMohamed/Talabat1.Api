using AutoMapper;
using Domain.Contracts;
using Services.Abstraction;

namespace Services
{
	public class ServicesManager(IUnitOfWork unitOfWork,IMapper mapper) : IServicesManager
	{
		public IProductService ProductService { get; } = new ProductService(unitOfWork,mapper);
	}
}
