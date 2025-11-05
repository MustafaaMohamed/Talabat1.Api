using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
	public class OrderWithPaymentIntentSpecification : BaseSpecification<Order, Guid>
	{
		public OrderWithPaymentIntentSpecification(string PaymentIntentId) : base(o=>o.PaymentIntentId == PaymentIntentId)
		{
		}
	}
}
