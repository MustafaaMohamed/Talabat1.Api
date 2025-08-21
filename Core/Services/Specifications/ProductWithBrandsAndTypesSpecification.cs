using Domain.Models;
using Shared;

namespace Services.Specifications
{
	public class ProductWithBrandsAndTypesSpecification : BaseSpecification<Product, int>
	{
		public ProductWithBrandsAndTypesSpecification(int id) : base(p=>p.Id == id)
		{
			AddInclude(p=>p.ProductBrand);
			AddInclude(p => p.ProductType);
		}
		public ProductWithBrandsAndTypesSpecification(ProductSpecificationParameters specParams)
			: base(p =>
						(string.IsNullOrEmpty(specParams.SearchValue) || p.Name.ToLower().Contains(specParams.SearchValue.ToLower())) &&
						(!specParams.BrandId.HasValue || specParams.BrandId == p.BrandId) &&
						(!specParams.TypeId.HasValue || specParams.TypeId == p.TypeId))
		{
			AddInclude(p => p.ProductBrand);
			AddInclude(p => p.ProductType);
			AddSorting(specParams.Sort);
			ApplyPagination(specParams.PageIndex, specParams.PageSize);
		}
		private void AddSorting(string? sort)
		{
			if (!string.IsNullOrEmpty(sort))
			{
				switch (sort.ToLower())
				{
					case "namedesc":
						AddOrderByDescending(p => p.Name);
						break;
					case "priceasc":
						AddOrderBy(p => p.Price);
						break;
					case "pricdesc":
						AddOrderByDescending(p => p.Price);
						break;
					default:
						AddOrderBy(p => p.Name);
						break;
				}

			}
			else
			{
				AddOrderBy(p => p.Name);
			}
		}
		
	}
}
