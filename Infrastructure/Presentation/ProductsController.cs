using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstraction;
using Shared;
using Shared.ErrorModels;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController(IServicesManager servicesManager) : ControllerBase
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK,Type = typeof(PaginationResponse<ProductDto>))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError,Type = typeof(ErrorDetails))]
		[ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(ErrorDetails))]
		public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecificationParameters specParams)
		{
			var result = await servicesManager.ProductService.GetAllProductsAsync(specParams);
			if (result is null) return BadRequest();
			return Ok(result);
		}
		[HttpGet("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
		[Cache(100)]
		[Authorize]
		public async Task<ActionResult<ProductDto>> GetProductByIdAsync(int id)
		{
			var result = await servicesManager.ProductService.GetProductByIdAsync(id);
			if (result is null) return NotFound();
			return Ok(result);
		}
		[HttpGet("brands")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandDto>))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
		public async Task<ActionResult<BrandDto>> GetAllBrands()
		{
			var result = await servicesManager.ProductService.GetAllBrandsAsync();
			if (result is null) return NotFound();
			return Ok(result);
		}
		[HttpGet("types")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TypeDto>))]
		[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
		[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
		public async Task<ActionResult<TypeDto>> GetAllTypes()
		{
			var result = await servicesManager.ProductService.GetAllTypesAsync();
			if (result is null) return NotFound();
			return Ok(result);
		}

	}
}
