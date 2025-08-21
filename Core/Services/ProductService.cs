using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstraction;
using Services.Specifications;
using Shared;

namespace Services
{
	public class ProductService(IUnitOfWork unitOfWork,IMapper mapper) : IProductService
	{
		public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecificationParameters specParams)
		{
			var spec = new ProductWithBrandsAndTypesSpecification(specParams);
			var products= await unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
			var specCount = new ProductWithCountSpecification(specParams);
			var count = await unitOfWork.GetRepository<Product, int>().CountAsync(specCount);
			var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);
			return new PaginationResponse<ProductDto>(specParams.PageIndex, specParams.PageSize,count,productsDto);

		}
		public async Task<ProductDto?> GetProductByIdAsync(int id)
		{
			var spec = new ProductWithBrandsAndTypesSpecification(id);			
			var product = await unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);
			if (product is null) return null;
			var productDto = mapper.Map<ProductDto>(product);
			return productDto;
			
		}
		public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
		{
			var spec = new BrandsSpecification();
			var brands = await unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(spec);
			var brandsDto = mapper.Map<IEnumerable<BrandDto>>(brands);
			return brandsDto;
		}
		public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
		{
			var spec = new TypesSpecification();
			var types = await unitOfWork.GetRepository<ProductType, int>().GetAllAsync(spec);
			var typesDto = mapper.Map<IEnumerable<TypeDto>>(types);
			return typesDto;
		}

		
	}
}
