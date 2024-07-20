using ProgQuizWebsite.Api.Notifications.ResponseModels;
using ProgQuizWebsite.Domain.Notifications.Models;

namespace ProgQuizWebsite.Services.Notifications.Interfaces
{
	public interface INotificationsService
	{
		Task<List<Notification>> GetAllAsync();
		Task<UserNotificationsResponse> GetUserNotificationsAsync(Guid userId);
		Task<Guid> NotifyUsersAsync(Notification notification);
		Task<NotifyUserResponse> NotifyUserAsync(Notification notification, Guid userId);
	}
}
