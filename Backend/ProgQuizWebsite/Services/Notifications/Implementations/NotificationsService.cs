using Core.Constants;
using ProgQuizWebsite.Api.Notifications.ResponseModels;
using ProgQuizWebsite.Domain.Notifications.FilterModels;
using ProgQuizWebsite.Domain.Notifications.Interfaces;
using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using ProgQuizWebsite.Services.Notifications.Interfaces;
using UserService.Services.Interfaces;

namespace ProgQuizWebsite.Services.Notifications.Implementations
{
    internal class NotificationsService : INotificationsService
	{
		private readonly INotificationsRepository _notificationsRepository;
		private readonly IUsersService _usersService;

		public NotificationsService(INotificationsRepository notificationRepository, IUsersService usersService)
		{
			_notificationsRepository = notificationRepository;
			_usersService = usersService;
		}

		public async Task<List<Notification>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return (await _notificationsRepository.GetAllAsync(cancellationToken)).OrderByDescending(n => n.Date).ToList();
		}

		public async Task<UserNotificationsResponse> GetUserNotificationsAsync(Guid userId, NotificationsFilter notificationsFilter,
			HttpResponse httpResponse)
		{
			if (notificationsFilter is null || notificationsFilter.Page < 1) 
				return new UserNotificationsResponse() { ResponseCode = Core.Enums.ResponseCode.NoResult, Notifications = new List<Notification>() };
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new UserNotificationsResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь"
			};
			var notifications = user.Notifications.OrderByDescending(n => n.Date).Skip(DataRestrictions.NotificationsPerPageQuantity * (notificationsFilter.Page - 1))
				.Take(DataRestrictions.NotificationsPerPageQuantity).ToList();
			httpResponse.Headers.Add(SpecialConstants.ContentCountHeaderName, user.Notifications.Count.ToString());
			return new UserNotificationsResponse() { ResponseCode = Core.Enums.ResponseCode.Success, Notifications = notifications };
		}

		public async Task<NotifyUserResponse> NotifyUserAsync(Notification notification, Guid userId)
		{
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new NotifyUserResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь"
			};
			if (!user.UserNotificationsInfo.ReceiveNotifications) return new NotifyUserResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.BadRequest,
				ErrorMessage = "Пользователь не принимает уведомления"
			};
			notification.Users = new List<User>{ user };
			user.UserNotificationsInfo.NewNotificationsCount++;
			var id = await _notificationsRepository.AddAsync(notification);
			return new NotifyUserResponse() { ResponseCode = Core.Enums.ResponseCode.Success, Id = id };
		}

		public async Task<Guid> NotifyUsersAsync(Notification notification)
		{
			var users = await _usersService.GetNotificationSubscribers();
			notification.Users = new List<User>();
			foreach (var user in users)
			{
				notification.Users.Add(user);
				user.UserNotificationsInfo.NewNotificationsCount++;
			}
			return await _notificationsRepository.AddAsync(notification);
		}

		public async Task<UserNotificationsRemoveResponse> ClearUserNotificationsAsync(Guid userId)
		{
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new UserNotificationsRemoveResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь"
			};
			user.Notifications.Clear();
			user.UserNotificationsInfo.NewNotificationsCount = 0;
			await _notificationsRepository.SaveChangesAsync();
			return new UserNotificationsRemoveResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}

		public async Task<UserNotificationRemoveResponse> RemoveUserNotificationAsync(Guid notificationId, Guid userId)
		{
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new UserNotificationRemoveResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь"
			};
			var notification = await GetByGuidAsync(notificationId);
			if (notification == null) return new UserNotificationRemoveResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Попытка удалить несуществующее уведомление"
			};
			user.Notifications.Remove(notification);
			await _notificationsRepository.SaveChangesAsync();
			return new UserNotificationRemoveResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}

		public async Task<Notification> GetByGuidAsync(Guid id)
		{
			return await _notificationsRepository.GetByGuidAsync(id);
		}
	}
}
