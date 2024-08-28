using ProgQuizWebsite.Domain.Users.Models.UserModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgQuizWebsite.Domain.Users.Models.ResetPasswordRequests
{
	[Table("PasswordRequests")]
	public class ResetPasswordRequest
	{
		public Guid Id { get; set; }
		public string Sequence { get; set; }
		public DateTime ExpirationDate { get; set; }
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
