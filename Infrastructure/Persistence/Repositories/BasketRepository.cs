using Domain.Contracts;
using Domain.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
	public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
	{
		private readonly IDatabase _database = connection.GetDatabase();
		public async Task<CustomerBasket?> GetBasketByIdAsync(string id)
		{
			var redisValue= await _database.StringGetAsync(id);
			if (redisValue.IsNullOrEmpty) return null;
			var basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
			if (basket is null) return null;
			return basket;
		}
		public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket customerBasket, TimeSpan? timeToLive = null)
		{
			var redisValue = JsonSerializer.Serialize(customerBasket);
			var flag = await _database.StringSetAsync(customerBasket.Id, redisValue, TimeSpan.FromDays(30));
			return flag ? await GetBasketByIdAsync(customerBasket.Id) : null;
		}
		public async Task<bool> DeleteBasketAsync(string id)
		{
			return await _database.KeyDeleteAsync(id);
		}

	}
}
