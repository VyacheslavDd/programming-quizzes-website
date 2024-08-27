using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;

namespace ProgQuizWebsite.Services.Quizzes.Interfaces
{
	public interface IBusQuizNotificationsService
	{
		Task DoNewQuizNotificationsAsync(Quiz quiz, Guid quizId);
		Task DoWebsiteNotificationsAsync(Quiz quiz, string languageCategory);
		Task DoEmailNotificationsAsync(Quiz quiz, Guid quizId, string languageCategory);
	}
}
