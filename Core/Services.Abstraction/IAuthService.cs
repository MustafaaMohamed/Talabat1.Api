using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
	public interface IAuthService
	{
		Task<UserResultDto> LoginAsync(LoginDto loginDto);
		Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
		Task<bool> CheckEmailExistAsync(string userEmail);
		Task<UserResultDto> GetCurrentUserAsync(string userEmail);
		Task<AddressDto> GetCurrentUserAddressAsync(string userEmail);
		Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto,string userEmail);

	}
}
