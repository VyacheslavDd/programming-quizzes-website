using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Quizzes.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Repositories
{
	internal class QuizRatingRepository : IQuizRatingRepository
	{
		private readonly QuizAppContext _quizAppContext;

		public QuizRatingRepository(QuizAppContext quizAppContext)
		{
			_quizAppContext = quizAppContext;
		}

		public async Task<Guid> AddRatingAsync(QuizRating quizRating)
		{
			var entity = await _quizAppContext.QuizRatings.AddAsync(quizRating);
			return entity.Entity.Id;
		}

		public async Task<QuizRating?> GetRatingAsync(Guid userId, Guid quizId)
		{
			return await _quizAppContext.QuizRatings.FirstOrDefaultAsync(qr => qr.UserId == userId && qr.QuizId == quizId);
		}

		public async Task RemoveRatingAsync(Guid userId, Guid quizId)
		{
			var entity = await GetRatingAsync(userId, quizId);
			_quizAppContext.QuizRatings.Remove(entity);
		}

		public async Task SaveChangesAsync()
		{
			await _quizAppContext.SaveChangesAsync();
		}
	}
}
