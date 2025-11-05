using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
	public class Order : BaseEntity<Guid>
	{
		public Order()
		{
		}

		public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
		{
			Id = Guid.NewGuid();
			UserEmail = userEmail;
			ShippingAddress = shippingAddress;
			OrderItems = orderItems;
			DeliveryMethod = deliveryMethod;
			SubTotal = subTotal;
			PaymentIntentId = paymentIntentId;
		}

		public string UserEmail { get; set; }
		public Address ShippingAddress { get; set; }
		public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Navigational Property
		public DeliveryMethod DeliveryMethod { get; set; } // Navigational Property
		public int? DeliveryMethodId { get; set; } //Foreign Key
		public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
		public decimal SubTotal { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
		public string PaymentIntentId { get; set; }

	}
}
