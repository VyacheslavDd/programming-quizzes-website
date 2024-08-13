using Core.Base;

namespace ProgQuizWebsite.Api.Quizzes.ResponseModels
{
	public class QuizRatingAddResponse : BaseHttpResponse
	{
		public required Guid Id { get; set; }
	}
}
