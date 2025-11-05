
namespace Services.Abstraction
{
	public interface IServicesManager
	{
		IProductService ProductService { get; }
		IBasketService BasketService { get; }
		ICacheService CacheService{ get; }
		IAuthService AuthService { get; }
		IOrderService OrderService { get; }
		IPaymentService PaymentService { get; }
	}
}
