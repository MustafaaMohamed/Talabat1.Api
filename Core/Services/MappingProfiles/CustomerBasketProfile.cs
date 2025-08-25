using AutoMapper;
using Domain.Models;
using Shared;

namespace Services.MappingProfiles
{
	public class CustomerBasketProfile : Profile
	{
		public CustomerBasketProfile()
		{
			CreateMap<CustomerBasket,CustomerBasketDto>().ReverseMap();
			CreateMap<BasketItem, BasketItemDto>().ReverseMap();
		}
	}
}
