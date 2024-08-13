using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;

namespace ProgQuizWebsite.Domain.Quizzes.Interfaces
{
	public interface IQuizRatingRepository
	{
		Task RemoveRatingAsync(Guid userId, Guid quizId);
		Task<Guid> AddRatingAsync(QuizRating quizRating);
		Task<QuizRating?> GetRatingAsync(Guid userId, Guid quizId);
		Task SaveChangesAsync();
	}
}
