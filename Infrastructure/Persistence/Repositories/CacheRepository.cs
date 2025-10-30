
using Domain.Contracts;
using StackExchange.Redis;
using System.Text.Json;

namespace Persistence.Repositories
{
	public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
	{
		private readonly IDatabase _database = connection.GetDatabase();
		public async Task<string?> GetAsync(string key)
		{
			var redisValue = await _database.StringGetAsync(key);
			return (!redisValue.IsNullOrEmpty) ? redisValue : default;
		}

		public async Task SetAsync(string key, object value, TimeSpan duration)
		{
			var redisValue = JsonSerializer.Serialize(value);
			await _database.StringSetAsync(key,redisValue,duration);
		}
	}
}
