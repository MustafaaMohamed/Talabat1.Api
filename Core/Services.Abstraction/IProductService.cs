using Shared;

namespace Services.Abstraction
{
	public interface IProductService
	{
		Task<IEnumerable<ProductDto>> GetAllProductsAsync();
		Task<ProductDto?> GetProductByIdAsync(int id);
		Task<IEnumerable<TypeDto>> GetAllTypesAsync();
		Task<IEnumerable<BrandDto>> GetAllBrandsAsync();

	}
}
