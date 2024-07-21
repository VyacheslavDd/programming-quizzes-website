using ProgQuizWebsite.Api.Notifications.ResponseModels;
using ProgQuizWebsite.Domain.Notifications.Interfaces;
using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Services.Notifications.Interfaces;
using UserService.Domain.Models;
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

		public async Task<List<Notification>> GetAllAsync()
		{
			return await _notificationsRepository.GetAllAsync();
		}

		public async Task<UserNotificationsResponse> GetUserNotificationsAsync(Guid userId)
		{
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new UserNotificationsResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь"
			};
			return new UserNotificationsResponse() { ResponseCode = Core.Enums.ResponseCode.Success, Notifications = user.Notifications };
		}

		public async Task<NotifyUserResponse> NotifyUserAsync(Notification notification, Guid userId)
		{
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new NotifyUserResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь"
			};
			if (!user.ReceiveNotifications) return new NotifyUserResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.BadRequest,
				ErrorMessage = "Пользователь не принимает уведомления"
			};
			notification.Users = new List<User>{ user };
			var id = await _notificationsRepository.AddAsync(notification);
			return new NotifyUserResponse() { ResponseCode = Core.Enums.ResponseCode.Success, Id = id };
		}

		public async Task<Guid> NotifyUsersAsync(Notification notification)
		{
			var users = await _usersService.GetNotificationSubscribers();
			notification.Users = new List<User>();
			foreach (var user in users)
				notification.Users.Add(user);
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
