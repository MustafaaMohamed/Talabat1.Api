using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.MappingProfiles
{
	public class ProductProfile : Profile
	{
		public ProductProfile()
		{
			CreateMap<Product, ProductDto>()
				.ForMember(d=>d.BrandName,o=>o.MapFrom(s=>s.ProductBrand.Name))
				.ForMember(d=>d.TypeName,o=>o.MapFrom(s=>s.ProductType.Name));


			CreateMap<ProductBrand, BrandDto>();
			CreateMap<ProductType, TypeDto>();
		}
	}
}
