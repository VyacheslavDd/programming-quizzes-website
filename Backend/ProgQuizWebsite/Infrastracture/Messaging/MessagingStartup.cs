using Core.Constants;
using MassTransit;
using Microsoft.Extensions.Configuration;
using ProgQuizWebsite.Infrastracture.Messaging.Consumers;

namespace ProgQuizWebsite.Infrastracture.Messaging
{
	public static class MessagingStartup
	{
		public static IServiceCollection AddMassTransitMessaging(this IServiceCollection services, IConfiguration config)
		{
			var rabbitMqConfiguration = config.GetRequiredSection("RabbitMq");
			services.AddMassTransit(config =>
			{
				config.SetKebabCaseEndpointNameFormatter();
				config.AddConsumer<NotificationsConsumer>();
				config.UsingRabbitMq((context, rabbitmq) =>
				{
					rabbitmq.Host(rabbitMqConfiguration.GetValue<string>("Host"), config =>
					{
						config.Username(rabbitMqConfiguration.GetValue<string>("Username"));
						config.Password(rabbitMqConfiguration.GetValue<string>("Password"));
					});
					rabbitmq.ReceiveEndpoint(SpecialConstants.NotificationsQueueName, notifyQueueConfig =>
					{
						notifyQueueConfig.ConfigureConsumer<NotificationsConsumer>(context);
					});
					rabbitmq.ConfigureEndpoints(context);
				});
			});
			return services;
		}
	}
}
