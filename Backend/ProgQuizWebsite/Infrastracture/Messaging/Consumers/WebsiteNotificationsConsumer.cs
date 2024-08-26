using AutoMapper;
using MassTransit;
using ProgQuizWebsite.Api.Notifications.PostModels;
using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Services.Notifications.Interfaces;

namespace ProgQuizWebsite.Infrastracture.Messaging.Consumers
{
	public class WebsiteNotificationsConsumer : IConsumer<SimpleNotificationPostModel>
	{
		private readonly INotificationsService _notificationsService;
		private readonly IMapper _mapper;

		public WebsiteNotificationsConsumer(INotificationsService notificationService, IMapper mapper)
		{
			_notificationsService = notificationService;
			_mapper = mapper;
		}

		public async Task Consume(ConsumeContext<SimpleNotificationPostModel> context)
		{
			var notification = _mapper.Map<Notification>(context.Message);
			await _notificationsService.NotifyUsersAsync(notification);
		}
	}
}
