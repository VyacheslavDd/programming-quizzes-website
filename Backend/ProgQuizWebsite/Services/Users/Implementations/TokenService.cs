using Core.Constants;
using Microsoft.IdentityModel.Tokens;
using ProgQuizWebsite.Api.Users.ResponseModels.RefreshTokens;
using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Services.Interfaces;

namespace UserService.Services.Implementations
{
    internal class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;
		private readonly IRefreshTokenRepository _refreshTokenRepository;

		public TokenService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository)
		{
			_configuration = configuration;
			_refreshTokenRepository = refreshTokenRepository;
		}

		public async Task<AddOrUpdateTokensResponse?> AddOrUpdateTokensAsync(User? user, RefreshToken? token = null)
		{
			if (user == null) return new AddOrUpdateTokensResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.BadRequest,
				ErrorMessage = "Указанный пользователь не существует",
				AccessToken = ""
			};
			var existingToken = token ?? await GetRefreshTokenByUserGuidAsync(user.Id);
			if (existingToken != null)
			{
				var (newAccessToken, newRefreshToken) = CreateTokens(user);
				existingToken.Token = newRefreshToken;
				existingToken.ExpirationDate = DateTime.UtcNow.AddMonths(1);
				await _refreshTokenRepository.SaveChangesAsync();
				return new AddOrUpdateTokensResponse() { ResponseCode = Core.Enums.ResponseCode.Success, AccessToken = newAccessToken };
			}
			var (accessToken, refreshToken) = CreateTokens(user);
			var refreshTokenModel = new RefreshToken() { Id = new Guid(), Token = refreshToken, ExpirationDate = DateTime.UtcNow.AddMonths(1) };
			user.RefreshToken = refreshTokenModel;
			refreshTokenModel.User = user;
			await _refreshTokenRepository.AddRefreshTokenAsync(refreshTokenModel);
			await _refreshTokenRepository.SaveChangesAsync();
			return new AddOrUpdateTokensResponse() { ResponseCode = Core.Enums.ResponseCode.Success, AccessToken = accessToken };
		}

		public string CreateToken(User user, DateTime expirationDate)
		{
			var userClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.UserInfo.Login),
				new Claim("Id", user.Id.ToString())
			};
			foreach (var role in user.Roles)
				userClaims.Add(new Claim(ClaimTypes.Role, role.Name));
			var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration
				.GetSection(SpecialConstants.JwtTokenKeyPath).Value));
			var credentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
			var securityToken = new JwtSecurityToken(claims: userClaims, expires: expirationDate, signingCredentials: credentials);
			var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
			return token;
		}

		public Tuple<string, string> CreateTokens(User user)
		{
			var refreshToken = CreateToken(user, DateTime.Now.AddMonths(1));
			var accessToken = CreateToken(user, DateTime.Now.AddMinutes(15));
			return new Tuple<string, string>(accessToken, refreshToken);
		}

		public async Task<RefreshToken?> GetRefreshTokenByUserGuidAsync(Guid? userId)
		{
			var token = await _refreshTokenRepository.GetByUserGuidAsync(userId);
			return token;
		}

		public async Task<AddOrUpdateTokensResponse?> RefreshTokensAsync(Guid? userId)
		{
			var token = await GetRefreshTokenByUserGuidAsync(userId);
			if (token == null || token.ExpirationDate < DateTime.UtcNow)
				return new AddOrUpdateTokensResponse()
				{
					ResponseCode = Core.Enums.ResponseCode.BadRequest,
					AccessToken = ""
				};
			return await AddOrUpdateTokensAsync(token.User, token);
		}
	}
}
