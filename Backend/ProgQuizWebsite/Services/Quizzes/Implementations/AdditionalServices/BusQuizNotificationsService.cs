using AutoMapper;
using Core.Base.Service.Interfaces;
using MassTransit;
using ProgQuizWebsite.Api.Notifications.PostModels;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Services.Quizzes.Interfaces;

namespace ProgQuizWebsite.Services.Quizzes.Implementations.AdditionalServices
{
	public class BusQuizNotificationsService : IBusQuizNotificationsService
	{
		private readonly IBus _messagingBus;
		private readonly ICategoryService _languageCategoryService;

		public BusQuizNotificationsService(IBus messagingBus, ICategoryService languageCategoryService)
		{
			_messagingBus = messagingBus;
			_languageCategoryService = languageCategoryService;
		}

		public async Task DoEmailNotificationsAsync(Quiz quiz, Guid quizId, string languageCategory)
		{
			var emailMessage = new NewQuizEmailNotificationModel()
			{
				LanguageCategory = languageCategory,
				QuizId = quizId.ToString(),
				Title = quiz.Title
			};
			await _messagingBus.Publish(emailMessage);
		}

		public async Task DoNewQuizNotificationsAsync(Quiz quiz, Guid quizId)
		{
			var languageCategory = await _languageCategoryService.GetByGuidAsync(quiz.LanguageCategoryId);
			await DoWebsiteNotificationsAsync(quiz, languageCategory?.Name);
			await DoEmailNotificationsAsync(quiz, quizId, languageCategory?.Name);
		}

		public async Task DoWebsiteNotificationsAsync(Quiz quiz, string languageCategory)
		{
			var simpleMessage = new SimpleNotificationPostModel()
			{ Content = $"На сайте появилась новая викторина {quiz.Title} ({languageCategory})" };
			await _messagingBus.Publish(simpleMessage);
		}
	}
}
