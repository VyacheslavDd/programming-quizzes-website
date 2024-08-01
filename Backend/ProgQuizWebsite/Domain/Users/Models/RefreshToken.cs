using UserService.Domain.Models;

namespace ProgQuizWebsite.Domain.Users.Models
{
	public class RefreshToken
	{
		public required Guid Id { get; set; }
		public required string Token { get; set; }
		public required DateTime ExpirationDate { get; set; }
		public Guid UserId { get; set; }
		public User? User { get; set; }
	}
}
