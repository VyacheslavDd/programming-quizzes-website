using ProgQuizWebsite.Domain.Notifications.Interfaces;
using ProgQuizWebsite.Infrastracture.Notifications.Repositories;

namespace ProgQuizWebsite.Infrastracture.Notifications.Startups
{
	public static class DomainStartup
	{
		public static IServiceCollection AddNotificationsDomain(this IServiceCollection services)
		{
			services.AddScoped<INotificationsRepository, NotificationsRepository>();
			return services;
		}
	}
}
