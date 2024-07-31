using ProgQuizWebsite.Api.Notifications.ResponseModels;
using ProgQuizWebsite.Domain.Notifications.FilterModels;
using ProgQuizWebsite.Domain.Notifications.Models;

namespace ProgQuizWebsite.Services.Notifications.Interfaces
{
	public interface INotificationsService
	{
		Task<List<Notification>> GetAllAsync();
		Task<Notification> GetByGuidAsync(Guid id);
		Task<UserNotificationsResponse> GetUserNotificationsAsync(Guid userId, NotificationsFilter notificationsFilter, HttpResponse httpResponse);
		Task<Guid> NotifyUsersAsync(Notification notification);
		Task<NotifyUserResponse> NotifyUserAsync(Notification notification, Guid userId);
		Task<UserNotificationsRemoveResponse> ClearUserNotificationsAsync(Guid userId);
		Task<UserNotificationRemoveResponse> RemoveUserNotificationAsync(Guid notificationId,  Guid userId);
	}
}
