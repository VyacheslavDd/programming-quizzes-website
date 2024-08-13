namespace ProgQuizWebsite.Api.Quizzes.PostModels.QuizRatings
{
	public class QuizRatingUpdateModel
	{
		public required Guid UserId { get; set; }
		public required Guid QuizId { get; set; }
		public required int Rating { get; set; }
	}
}
