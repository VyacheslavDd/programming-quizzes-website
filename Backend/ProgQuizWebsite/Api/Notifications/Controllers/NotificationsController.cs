using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.Notifications.PostModels;
using ProgQuizWebsite.Api.Notifications.ResponseModels;
using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Services.Notifications.Interfaces;

namespace ProgQuizWebsite.Api.Notifications.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NotificationsController : ControllerBase
	{
		private readonly INotificationsService _notificationsService;
		private readonly IMapper _mapper;

		public NotificationsController(INotificationsService notificationService, IMapper mapper)
		{
			_notificationsService = notificationService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить все уведомления
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync()
		{
			var notifications = await _notificationsService.GetAllAsync();
			var mappedNotifications = _mapper.Map<List<NotificationResponseModel>>(notifications);
			return Ok(mappedNotifications);
		}

		/// <summary>
		/// Получить все уведомления пользователя
		/// </summary>
		/// <param name="userId">Guid пользователя</param>
		/// <returns></returns>
		[HttpGet]
		[Route("{userId}/all")]
		public async Task<IActionResult> GetUserNotificationsAsync([FromRoute] Guid userId)
		{
			var notificationsResponse = await _notificationsService.GetUserNotificationsAsync(userId);
			var mappedNotifications = _mapper.Map<List<NotificationResponseModel>>(notificationsResponse.Notifications);
			return Ok(new
			{
				notificationsResponse.ResponseCode,
				notificationsResponse.ErrorMessage,
				Notifications = mappedNotifications
			});
		}

		/// <summary>
		/// Уведомить всех пользователей, подписанных на уведомления
		/// </summary>
		/// <param name="notificationPostModel">Модель уведомления</param>
		/// <returns></returns>
		[HttpPost]
		[Route("notify")]
		public async Task<IActionResult> NotifyUsersAsync([FromBody] NotificationPostModel notificationPostModel)
		{
			var notification = _mapper.Map<Notification>(notificationPostModel);
			var guid = await _notificationsService.NotifyUsersAsync(notification);
			return Ok(guid);
		}

		/// <summary>
		/// Уведомить конкретного пользователя
		/// </summary>
		/// <param name="userId">Guid пользователя</param>
		/// <param name="notificationPostModel">Модель уведомления</param>
		/// <returns></returns>
		[HttpPost]
		[Route("notify/{userId}")]
		public async Task<IActionResult> NotifyUserAsync([FromRoute] Guid userId, [FromBody] NotificationPostModel notificationPostModel)
		{
			var notification = _mapper.Map<Notification>(notificationPostModel);
			var response = await _notificationsService.NotifyUserAsync(notification, userId);
			return Ok(response);
		}
	}
}
