using Domain.Models;

namespace Domain.Contracts
{
	public interface IBasketRepository
	{
		Task<CustomerBasket?> GetBasketByIdAsync(string id);
		Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket,TimeSpan? timeToLive = null);
		Task<bool> DeleteBasketAsync(string id);

	}
}
