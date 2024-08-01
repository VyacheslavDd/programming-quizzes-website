using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Domain.Users.Models;

namespace UserService.Domain.Models
{
	public class User
	{
		public required Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public long PhoneNumber { get; set; }
		public required string Email { get; set; }
		public required string Login { get; set; }
		public required string PasswordHash { get; set; }
		public bool ReceiveNotifications { get; set; }
		public List<Role> Roles { get; set; }
		public List<Notification> Notifications { get; set; }
		public RefreshToken? RefreshToken { get; set; }
	}
}
