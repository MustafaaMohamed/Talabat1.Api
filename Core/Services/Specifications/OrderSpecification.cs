using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
	internal class OrderSpecification : BaseSpecification<Order, Guid>
	{
		public OrderSpecification(Guid id) : base(o=>o.Id == id)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.OrderItems);
		}
		public OrderSpecification(string userEmail) : base(o => o.UserEmail == userEmail)
		{
			AddInclude(o => o.DeliveryMethod);
			AddInclude(o => o.OrderItems);
		}
	}
}
