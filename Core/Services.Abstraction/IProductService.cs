using Shared;

namespace Services.Abstraction
{
	public interface IProductService
	{
		Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecificationParameters specParams);
		Task<ProductDto?> GetProductByIdAsync(int id);
		Task<IEnumerable<TypeDto>> GetAllTypesAsync();
		Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

	}
}
