
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class BasketsController(IServicesManager servicesManager) : ControllerBase
	{
		
		[HttpGet]
		public async Task<IActionResult> GetBasketById(string id)
		{
			var result = await servicesManager.BasketService.GetBasketByIdAsync(id);
			return Ok(result);
		}
		[HttpPost]
		public async Task<IActionResult> UpdateBasket(CustomerBasketDto basketDto)
		{
			var result = await servicesManager.BasketService.UpdateBasketAsync(basketDto);
			return Ok(result);
		}
		[HttpDelete]
		public async Task<IActionResult> DeleteBasket(string id)
		{
			var result = await servicesManager.BasketService.DeleteBasketAsync(id);
			return NoContent();
		}
	}
}
