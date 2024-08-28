using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models.ResetPasswordRequests;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Users.Repositories
{
	internal class ResetPasswordRequestRepository : IResetPasswordRequestRepository
	{
		private readonly QuizAppContext _quizAppContext;

		public ResetPasswordRequestRepository(QuizAppContext quizAppContext)
		{
			_quizAppContext = quizAppContext;
		}

		public async Task AddAsync(ResetPasswordRequest request)
		{
			await _quizAppContext.ResetPasswordRequests.AddAsync(request);
		}

		public async Task DeleteAsync(Guid userId)
		{
			await _quizAppContext.ResetPasswordRequests.Where(rpr => rpr.UserId == userId).ExecuteDeleteAsync();
		}

		public async Task<ResetPasswordRequest?> GetBySequenceAsync(string sequence)
		{
			return await _quizAppContext.ResetPasswordRequests.FirstOrDefaultAsync(rpr => rpr.Sequence == sequence);
		}

		public async Task SaveChangesAsync()
		{
			await _quizAppContext.SaveChangesAsync();
		}
	}
}
