using ProgQuizWebsite.Api.Users.ResponseModels.RefreshTokens;
using ProgQuizWebsite.Domain.Users.Models;
using ProgQuizWebsite.Domain.Users.Models.UserModel;

namespace UserService.Services.Interfaces
{
    public interface ITokenService
	{
		string CreateToken(User user, DateTime expirationDate);
		Task<RefreshToken?> GetRefreshTokenByUserGuidAsync(Guid? userId);
		Task<AddOrUpdateTokensResponse?> AddOrUpdateTokensAsync(User? user, RefreshToken? token = null);
		Tuple<string, string> CreateTokens(User user);
		Task<AddOrUpdateTokensResponse?> RefreshTokensAsync(Guid? userId);
	}
}
