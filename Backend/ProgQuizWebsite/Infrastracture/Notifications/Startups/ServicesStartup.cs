using ProgQuizWebsite.Services.Notifications.Implementations;
using ProgQuizWebsite.Services.Notifications.Interfaces;

namespace ProgQuizWebsite.Infrastracture.Notifications.Startups
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddNotificationsServices(this IServiceCollection services)
		{
			services.AddScoped<INotificationsService, NotificationsService>();
			return services;
		}
	}
}
