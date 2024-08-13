using ProgQuizWebsite.Api.Quizzes.PostModels.QuizRatings;
using ProgQuizWebsite.Api.Quizzes.ResponseModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork;
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using UserService.Services.Interfaces;

namespace ProgQuizWebsite.Services.Quizzes.Implementations.MainServices
{
	public class QuizRatingService : IQuizRatingService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IUsersService _usersService;

		public QuizRatingService(IUnitOfWork unitOfWork, IUsersService usersService)
		{
			_unitOfWork = unitOfWork;
			_usersService = usersService;
		}

		public async Task<QuizRatingAddResponse> AddQuizRatingAsync(QuizRating quizRating)
		{
			var quiz = await _unitOfWork.QuizRepository.GetByGuidAsync(quizRating.QuizId);
			var user = await _usersService.FindByGuidAsync(quizRating.UserId);
			user.QuizRatings.Add(quizRating);
			quiz.QuizRatings.Add(quizRating);
			quiz.QuizRatingsInfo.RatedByCount += 1;
			quiz.QuizRatingsInfo.RatingSum += quizRating.Rating;
			var guid = await _unitOfWork.QuizRatingRepository.AddRatingAsync(quizRating);
			await _unitOfWork.SaveAsync();
			return new QuizRatingAddResponse() { ResponseCode = Core.Enums.ResponseCode.Created, Id = guid };
		}

		public async Task<QuizRating?> GetQuizRatingAsync(Guid userId, Guid quizId)
		{
			return await _unitOfWork.QuizRatingRepository.GetRatingAsync(userId, quizId);
		}

		public async Task<QuizRatingRemoveResponse> RemoveQuizRatingAsync(Guid userId, Guid quizId, int rating)
		{
			var quiz = await _unitOfWork.QuizRepository.GetByGuidAsync(quizId);
			await _unitOfWork.QuizRatingRepository.RemoveRatingAsync(userId, quizId);
			quiz.QuizRatingsInfo.RatingSum -= rating;
			quiz.QuizRatingsInfo.RatedByCount -= 1;
			await _unitOfWork.SaveAsync();
			return new QuizRatingRemoveResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}

		public async Task<QuizRatingUpdateResponse> UpdateQuizRatingAsync(QuizRating quizRating)
		{
			var quiz = await _unitOfWork.QuizRepository.GetByGuidAsync(quizRating.QuizId);
			var rating = await GetQuizRatingAsync(quizRating.UserId, quizRating.QuizId);
			quiz.QuizRatingsInfo.RatingSum += quizRating.Rating - rating.Rating;
			rating.Rating = quizRating.Rating;
			await _unitOfWork.SaveAsync();
			return new QuizRatingUpdateResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}
	}
}
