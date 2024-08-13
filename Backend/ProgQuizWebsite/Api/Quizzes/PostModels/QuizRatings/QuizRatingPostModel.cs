namespace ProgQuizWebsite.Api.Quizzes.PostModels.QuizRatings
{
	public class QuizRatingPostModel
	{
		public required Guid UserId { get; set; }
		public required Guid QuizId { get; set; }
		public required int Rating { get; set; }
	}
}
