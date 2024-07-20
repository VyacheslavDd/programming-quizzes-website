using Core.Base;
using ProgQuizWebsite.Domain.Notifications.Models;

namespace ProgQuizWebsite.Api.Notifications.ResponseModels
{
	public class UserNotificationsResponse : BaseHttpResponse
	{
		public List<Notification> Notifications { get; set; }
	}
}
