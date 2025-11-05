using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class OrdersController(IServicesManager servicesManager) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> CreateOrder(OrderRequestDto orderRequestDto)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var result = await servicesManager.OrderService.CreateOrderAsync(orderRequestDto, email);
			return Ok(result);
		}
		[HttpGet]
		public async Task<IActionResult> GetOrdersByUserEmail()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var result = await servicesManager.OrderService.GetOrdersByUserEmailAsync(email);
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrdersByUserEmail(Guid id)
		{
			var result = await servicesManager.OrderService.GetOrderByIdAsync(id);
			return Ok(result);
		}

		[HttpGet("DeliveryMethods")]

		public async Task<IActionResult> GetDeliveryMethods()
		{
			var result = await servicesManager.OrderService.GetAllDeliveryMethods();
			return Ok(result);

		}
	}
}
