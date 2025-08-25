
using Shared;

namespace Services.Abstraction
{
	public interface IBasketService
	{
		Task<CustomerBasketDto> GetBasketByIdAsync(string id);
		Task<CustomerBasketDto> UpdateBasketAsync(CustomerBasketDto basketDto);
		Task<bool> DeleteBasketAsync(string id);
	}
}
