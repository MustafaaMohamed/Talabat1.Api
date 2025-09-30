
namespace Domain.Contracts
{
	public interface ICacheRepository
	{
		Task SetAsync(string key,object value,TimeSpan duration);
		Task<string?> GetAsync(string key);
	}
}
