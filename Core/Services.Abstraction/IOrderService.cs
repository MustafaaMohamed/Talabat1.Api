using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
	public interface IOrderService
	{
		Task<OrderResultDto> GetOrderByIdAsync(Guid id);
		Task<IEnumerable<OrderResultDto>> GetOrdersByUserEmailAsync(string userEmail);
		Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequestDto,string userEmail);
		Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();
	}
}
