using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class PaymentsController(IServicesManager servicesManager) : ControllerBase
	{
		[HttpPost("{basketId}")]
		[Authorize]
		public async Task<IActionResult> CreateOrUpdatePaymentIntent(string basketId)
		{
			var result = await servicesManager.PaymentService.CreateOrUpdatePaymentIntentAsync(basketId);
			return Ok(result);
		}

	}
}
