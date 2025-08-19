using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Shared;

namespace Services
{
	public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
	{
		public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
		{
			var products= await unitOfWork.GetRepository<Product, int>().GetAllAsync();
			var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
			return productsDto;

		}
		public async Task<ProductDto?> GetProductByIdAsync(int id)
		{
			var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(id);
			if (product is null) return null;
			var productDto = mapper.Map<ProductDto>(product);
			return productDto;
		}
		public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
		{
			var brands= await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
			var brandsDto = mapper.Map<IEnumerable<BrandDto>>(brands);
			return brandsDto;
		}
		public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
		{
			var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
			var typesDto = mapper.Map<IEnumerable<TypeDto>>(types);
			return typesDto;
		}

		
	}
}
