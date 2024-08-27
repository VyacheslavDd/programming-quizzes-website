using Core.Emailing.Services;
using MassTransit;
using ProgQuizWebsite.Api.Notifications.PostModels;
using ProgQuizWebsite.Services.Notifications.Interfaces;
using UserService.Services.Interfaces;

namespace ProgQuizWebsite.Infrastracture.Messaging.Consumers
{
	public class QuizEmailNotificationsConsumer : IConsumer<NewQuizEmailNotificationModel>
	{
		private readonly IUsersService _usersService;
		private readonly IEmailService _emailService;

		public QuizEmailNotificationsConsumer(IUsersService usersService, IEmailService emailService)
		{
			_usersService = usersService;
			_emailService = emailService;
		}

		public async Task Consume(ConsumeContext<NewQuizEmailNotificationModel> context)
		{
			var users = await _usersService.GetNotificationSubscribers();
			var messageData = context.Message;
			foreach (var user in users)
			{
				await _emailService.SendNewQuizPublishedEmailAsync(user.UserInfo.Login, user.UserInfo.Email, messageData.Title,
					messageData.LanguageCategory, messageData.QuizId);
			}
		}
	}
}
