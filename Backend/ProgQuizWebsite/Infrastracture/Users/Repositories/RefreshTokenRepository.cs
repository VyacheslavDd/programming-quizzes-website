using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Api.Users.ResponseModels.RefreshTokens;
using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Users.Repositories
{
	internal class RefreshTokenRepository : IRefreshTokenRepository
	{
		private readonly QuizAppContext _quizAppContext;

		public RefreshTokenRepository(QuizAppContext quizAppContext)
		{
			_quizAppContext = quizAppContext;
		}

		public async Task<Guid> AddRefreshTokenAsync(RefreshToken refreshToken)
		{
			var entity = await _quizAppContext.RefreshTokens.AddAsync(refreshToken);
			return entity.Entity.Id;
		}

		public async Task<RefreshToken?> GetByUserGuidAsync(Guid? userId)
		{
			return await _quizAppContext.RefreshTokens.Include(t => t.User).ThenInclude(u => u.Roles)
				.FirstOrDefaultAsync(t => t.UserId == userId);
		}

		public async Task SaveChangesAsync()
		{
			await _quizAppContext.SaveChangesAsync();
		}
	}
}
