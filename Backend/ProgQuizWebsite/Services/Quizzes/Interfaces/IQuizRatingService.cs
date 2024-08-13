using ProgQuizWebsite.Api.Quizzes.PostModels.QuizRatings;
using ProgQuizWebsite.Api.Quizzes.ResponseModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;

namespace ProgQuizWebsite.Services.Quizzes.Interfaces
{
	public interface IQuizRatingService
	{
		Task<QuizRatingAddResponse> AddQuizRatingAsync(QuizRating quizRating);
		Task<QuizRating?> GetQuizRatingAsync(Guid userId, Guid quizId);
		Task<QuizRatingUpdateResponse> UpdateQuizRatingAsync(QuizRating quizRating);
		Task<QuizRatingRemoveResponse> RemoveQuizRatingAsync(Guid userId, Guid quizId, int rating);
	}
}
