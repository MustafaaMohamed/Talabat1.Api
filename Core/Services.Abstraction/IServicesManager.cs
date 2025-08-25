
namespace Services.Abstraction
{
	public interface IServicesManager
	{
		IProductService ProductService { get; }
		IBasketService BasketService { get; }
	}
}
