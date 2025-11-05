using Domain.Exceptions;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Services.Abstraction;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
namespace Services
{
	public class AuthService(UserManager<AppUser> userManager,
		 IOptions<JwtOptions> options,
		 IMapper mapper) : IAuthService
	{
		public async Task<bool> CheckEmailExistAsync(string userEmail)
		{
			var user = await userManager.FindByEmailAsync(userEmail);
			if (user is not null)
				return true;
			return false;
		}

		public async Task<AddressDto> GetCurrentUserAddressAsync(string userEmail)
		{
			var user = await userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u=>u.Email == userEmail);
			if (user is null) throw new UserNotFoundException(userEmail);
			var addressDto = mapper.Map<AddressDto>(user.Address);
			return addressDto;
		}

		public async Task<UserResultDto> GetCurrentUserAsync(string userEmail)
		{
			var user = await userManager.FindByEmailAsync(userEmail);
			if (user is null) throw new UserNotFoundException(userEmail);
			return new UserResultDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await GenerateJwtTokenAsync(user)
			};
		}

		public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
		{
			var user = await userManager.FindByEmailAsync(loginDto.Email);
			if (user is null) throw new UnAuthorizedException();

			var flag = await userManager.CheckPasswordAsync(user, loginDto.Password);
			if (!flag) throw new UnAuthorizedException();
			return new UserResultDto()
			{
				DisplayName=user.DisplayName,
				Email=user.Email,
				Token = await GenerateJwtTokenAsync(user)
			};
		}

		public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
		{
			if(await CheckEmailExistAsync(registerDto.Email))
			{
				throw new DuplicatedEmailBadRequest(registerDto.Email);
			}
			var user = new AppUser()
			{
				DisplayName = registerDto.DisplayName,
				UserName = registerDto.UserName,
				Email = registerDto.Email,
				PhoneNumber = registerDto.PhoneNumber
			};
			var result = await userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(error => error.Description);
				throw new ValidationException(errors);
			}
			return new UserResultDto()
			{
				DisplayName = user.DisplayName,
				Email = user.Email,
				Token = await GenerateJwtTokenAsync(user)
			};
		}

		public async Task<AddressDto> UpdateCurrentUserAddressAsync(AddressDto addressDto, string userEmail)
		{
			var user = await userManager.FindByEmailAsync(userEmail);
			if (user is null) throw new UserNotFoundException(userEmail);
			if(user.Address is not null)
			{
				user.Address.FirstName = addressDto.FirstName;
				user.Address.LastName = addressDto.LastName;
				user.Address.Country = addressDto.Country;
				user.Address.City = addressDto.City;
				user.Address.Street = addressDto.Street;
			}
			else
			{
				var address = mapper.Map<Address>(addressDto);
				user.Address = address;
			}
			await userManager.UpdateAsync(user);
			return addressDto;
		}

		private async Task<string> GenerateJwtTokenAsync(AppUser user)
		{
			var jwtOptions = options.Value;
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name,user.UserName),
				new Claim(ClaimTypes.Email,user.Email)
			};
			var roles = await userManager.GetRolesAsync(user);
			foreach (var role in roles)
			{
				authClaims.Add(new Claim(ClaimTypes.Role, role));
			}

			var secrectKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
			var token = new JwtSecurityToken(
				issuer: jwtOptions.Issuer,
				audience: jwtOptions.Audience,
				claims: authClaims,
				expires: DateTime.UtcNow.AddDays(jwtOptions.DurationInDays),
				signingCredentials: new SigningCredentials(secrectKey, SecurityAlgorithms.HmacSha256Signature));
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
