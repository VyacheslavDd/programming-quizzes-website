using ProgQuizWebsite.Domain.Users.Models.UserModel;

namespace ProgQuizWebsite.Domain.Quizzes.Models.QuizModels
{
	public class QuizRating
	{
		public Guid Id { get; set; }
		public Guid QuizId { get; set; }
		public Guid UserId { get; set; }
		public int Rating { get; set; }
		public Quiz Quiz { get; set; }
		public User User { get; set; }
	}
}
