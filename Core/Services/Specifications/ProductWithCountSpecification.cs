using Domain.Models;
using Shared;

namespace Services.Specifications
{
	public class ProductWithCountSpecification : BaseSpecification<Product, int>
	{
		public ProductWithCountSpecification(ProductSpecificationParameters specParams) : base(p =>
						(string.IsNullOrEmpty(specParams.SearchValue) || p.Name.ToLower().Contains(specParams.SearchValue.ToLower())) &&
						(!specParams.BrandId.HasValue || specParams.BrandId == p.BrandId) &&
						(!specParams.TypeId.HasValue || specParams.TypeId == p.TypeId))
		{
		}
	}
}
