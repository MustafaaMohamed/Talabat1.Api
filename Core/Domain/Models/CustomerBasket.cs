
namespace Domain.Models
{
	public class CustomerBasket
	{
		public string Id { get; set; }
		public IEnumerable<BasketItem> Items { get; set; }
		public string? ClientSecret { get; set; }
		public string? PaymentIntentId{ get; set; }
		public int? DeliveryMethodId { get; set; }
		public decimal? ShippingPrice { get; set; }


	}
}
