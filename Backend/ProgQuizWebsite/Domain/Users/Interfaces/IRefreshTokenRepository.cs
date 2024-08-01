using ProgQuizWebsite.Api.Users.ResponseModels.RefreshTokens;
using ProgQuizWebsite.Domain.Users.Models;

namespace ProgQuizWebsite.Domain.Users.Interfaces
{
	public interface IRefreshTokenRepository
	{
		Task<RefreshToken?> GetByUserGuidAsync(Guid? userId);
		Task<Guid> AddRefreshTokenAsync(RefreshToken refreshToken);
		Task SaveChangesAsync();
	}
}
