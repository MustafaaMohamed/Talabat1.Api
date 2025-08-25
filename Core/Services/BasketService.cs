
using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstraction;
using Shared;

namespace Services
{
	public class BasketService(IBasketRepository basketRepository,IMapper mapper) : IBasketService
	{

		public async Task<CustomerBasketDto> GetBasketByIdAsync(string id)
		{
			var basket = await basketRepository.GetBasketByIdAsync(id) ?? throw new BasketNotFoundException(id);
			var result = mapper.Map<CustomerBasketDto>(basket);
			return result;
		}

		public async Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basketDto)
		{
			var basket = mapper.Map<CustomerBasket>(basketDto);
			basket = await basketRepository.UpdateBasketAsync(basket) ?? throw new BasketCreateOrUpdateBadRequestException();
			var result = mapper.Map<CustomerBasketDto>(basket);
			return result;
		}
		public async Task<bool> DeleteBasketAsync(string id)
		{
			var flag = await basketRepository.DeleteBasketAsync(id);
			if (!flag) throw new BasketDeleteBadRequestException();
			return flag;
		}
	}
}
