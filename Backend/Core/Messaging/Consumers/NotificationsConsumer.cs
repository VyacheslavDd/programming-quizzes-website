using Core.Messaging.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messaging.Consumers
{
	public class NotificationsConsumer : IConsumer<NotificationData>
	{
		private readonly ILogger<NotificationsConsumer> _logger;

		public NotificationsConsumer(ILogger<NotificationsConsumer> logger)
		{
			_logger = logger;
		}

		public Task Consume(ConsumeContext<NotificationData> context)
		{
			_logger.LogInformation("Received information: {NotifyInformation}", context.Message.Message);
			return Task.CompletedTask;
		}
	}
}
