using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;
using System.Security.Claims;

namespace Presentation
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController(IServicesManager servicesManager) : ControllerBase
	{
		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginDto loginDto)
		{
			var result = await servicesManager.AuthService.LoginAsync(loginDto);
			return Ok(result);
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterDto registerDto)
		{
			var result = await servicesManager.AuthService.RegisterAsync(registerDto);
			return Ok(result);
		}

		[HttpGet("EmailExists")]
		public async Task<IActionResult> CheckEmailExistAsync(string userEmail)
		{
			var result = await servicesManager.AuthService.CheckEmailExistAsync(userEmail);
			return Ok(result);
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> GetCurrentUserAsync()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var result = await servicesManager.AuthService.GetCurrentUserAsync(email);
			return Ok(result);
		}

		[HttpPut("Address")]
		[Authorize]

		public async Task<IActionResult> UpdateCurrentUserAddressAsync(AddressDto addressDto)
		{
			var email = User.FindFirstValue(ClaimTypes.Email);

			var result = await servicesManager.AuthService.UpdateCurrentUserAddressAsync(addressDto, email);
			return Ok(result);

		}


		[HttpGet("Address")]
		[Authorize]
		public async Task<IActionResult> GetCurrentUserAddressAsync()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var result = await servicesManager.AuthService.GetCurrentUserAddressAsync(email);
			return Ok(result);

		}

	}
}
