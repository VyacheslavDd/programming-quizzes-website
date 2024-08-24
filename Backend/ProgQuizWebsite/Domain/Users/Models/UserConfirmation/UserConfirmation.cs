using ProgQuizWebsite.Domain.Users.Models.UserModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgQuizWebsite.Domain.Users.Models.UserConfirm
{
	[Table("Confirmation")]
	public class UserConfirmation
	{
		public Guid Id { get; set; }
		public string Sequence { get; set; }
		public Guid UserId { get; set; }
		public User? User { get; set; }
	}
}
