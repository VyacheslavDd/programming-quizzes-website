using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models.UserConfirm;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Users.Repositories
{
	internal class ConfirmationRepository : IConfirmationRepository
	{
		private readonly QuizAppContext _quizAppContext;

		public ConfirmationRepository(QuizAppContext quizAppContext)
		{
			_quizAppContext = quizAppContext;
		}

		public async Task AddConfirmationAsync(UserConfirmation userConfirmation)
		{
			await _quizAppContext.UserConfirmations.AddAsync(userConfirmation);
		}

		public async Task<UserConfirmation> GetUserConfirmationBySequenceAsync(string sequence)
		{
			return await _quizAppContext.UserConfirmations.FirstOrDefaultAsync(uc => uc.Sequence == sequence);
		}

		public async Task RemoveConfirmationAsync(Guid userId)
		{
			await _quizAppContext.UserConfirmations.Where(uc => uc.UserId == userId).ExecuteDeleteAsync();
		}

		public async Task SaveChangesAsync()
		{
			await _quizAppContext.SaveChangesAsync();
		}
	}
}
