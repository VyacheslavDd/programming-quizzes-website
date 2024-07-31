using UserService.Domain.Models;

namespace ProgQuizWebsite.Domain.Notifications.Models
{
	public class Notification
	{
		public required Guid Id { get; set; }
		public required string Content { get; set; }
		public required DateTime Date {  get; set; }
		public List<User> Users { get; set; }
	}
}
