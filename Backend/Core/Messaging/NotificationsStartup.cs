using Core.Constants;
using Core.Messaging.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messaging
{
	public static class NotificationsStartup
	{
		public static IServiceCollection AddMasstransitNotifications(this IServiceCollection services, IConfiguration configuration)
		{
			var rabbitMqConfiguration = configuration.GetRequiredSection("RabbitMq");
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
