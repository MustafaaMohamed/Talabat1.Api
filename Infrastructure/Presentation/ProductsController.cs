using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController(IServicesManager servicesManager) : ControllerBase
	{
		[HttpGet]
		public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecificationParameters specParams)
		{
			var result = await servicesManager.ProductService.GetAllProductsAsync(specParams);
			if (result is null) return BadRequest();
			return Ok(result);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetProductByIdAsync(int id)
		{
			var result = await servicesManager.ProductService.GetProductByIdAsync(id);
			if (result is null) return NotFound();
			return Ok(result);
		}
		[HttpGet("brands")]
		public async Task<IActionResult> GetAllBrands()
		{
			var result = await servicesManager.ProductService.GetAllBrandsAsync();
			if (result is null) return NotFound();
			return Ok(result);
		}
		[HttpGet("types")]
		public async Task<IActionResult> GetAllTypes()
		{
			var result = await servicesManager.ProductService.GetAllTypesAsync();
			if (result is null) return NotFound();
			return Ok(result);
		}

	}
}
