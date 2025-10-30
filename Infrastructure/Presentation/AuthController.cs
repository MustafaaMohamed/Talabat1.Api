using Microsoft.AspNetCore.Mvc;
using Services.Abstraction;
using Shared;

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
	}
}
