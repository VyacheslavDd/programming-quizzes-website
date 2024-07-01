using Core.Constants;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Domain.Models;
using UserService.Services.Interfaces;

namespace UserService.Services.Implementations
{
	internal class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string CreateToken(User user)
		{
			var userClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.Login),
				new Claim("Id", user.Id.ToString())
			};
			foreach (var role in user.Roles)
				userClaims.Add(new Claim(ClaimTypes.Role, role.Name));
			var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
				.GetSection(SpecialConstants.JwtTokenKeyPath).Value));
			var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
			var securityToken = new JwtSecurityToken(claims: userClaims, expires: DateTime.Now.AddMinutes(15), signingCredentials: credentials);
			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
			return token;
		}
	}
}
