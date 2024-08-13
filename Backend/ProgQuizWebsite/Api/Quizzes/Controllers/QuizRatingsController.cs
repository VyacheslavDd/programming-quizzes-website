using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.Quizzes.PostModels.QuizRatings;
using ProgQuizWebsite.Api.Quizzes.ViewModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Services.Quizzes.Interfaces;

namespace ProgQuizWebsite.Api.Quizzes.Controllers
{
	[Route("api/quiz-ratings")]
	[ApiController]
	public class QuizRatingsController : ControllerBase
	{
		private readonly IQuizRatingService _quizRatingService;
		private readonly IMapper _mapper;

		public QuizRatingsController(IQuizRatingService quizRatingService, IMapper mapper)
		{
			_quizRatingService = quizRatingService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить оценку пользователя к данной викторине
		/// </summary>
		/// <param name="userId">Guid пользователя</param>
		/// <param name="quizId">Guid викторины</param>
		/// <returns></returns>
		[HttpGet]
		[Route("")]
		public async Task<IActionResult> GetQuizRatingAsync([FromQuery] Guid userId, [FromQuery] Guid quizId)
		{
			var rating = await _quizRatingService.GetQuizRatingAsync(userId, quizId);
			var ratingModel = _mapper.Map<QuizRatingViewModel>(rating);
			return Ok(ratingModel);
		}

		/// <summary>
		/// Оценить викторину
		/// </summary>
		/// <param name="quizRatingPostModel">Модель данных оценки викторины</param>
		/// <returns></returns>
		[HttpPost]
		[Route("rate")]
		public async Task<IActionResult> AddQuizRatingAsync([FromBody] QuizRatingPostModel quizRatingPostModel)
		{
			var rating = _mapper.Map<QuizRating>(quizRatingPostModel);
			var response = await _quizRatingService.AddQuizRatingAsync(rating);
			return new JsonResult(response);
		}

		/// <summary>
		/// Обновить оценку викторины от конкретного пользователя
		/// </summary>
		/// <param name="quizRatingUpdateModel">Модель данных обновления оценки викторины</param>
		/// <returns></returns>
		[HttpPatch]
		[Route("update")]
		public async Task<IActionResult> UpdateQuizRatingAsync([FromBody] QuizRatingUpdateModel quizRatingUpdateModel)
		{
			var rating = _mapper.Map<QuizRating>(quizRatingUpdateModel);
			var response = await _quizRatingService.UpdateQuizRatingAsync(rating);
			return new JsonResult(response);
		}

		/// <summary>
		/// Удалить оценку викторины от конкретного пользователя
		/// </summary>
		/// <param name="userId">Guid пользователя</param>
		/// <param name="quizId">Guid викторины</param>
		/// <param name="rating">Удалённое значение</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("remove")]
		public async Task<IActionResult> RemoveQuizRatingAsync([FromQuery] Guid userId, [FromQuery] Guid quizId, [FromQuery] int rating)
		{
			await _quizRatingService.RemoveQuizRatingAsync(userId, quizId, rating);
			return Ok();
		}
	}
}
