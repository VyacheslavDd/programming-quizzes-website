using ProgQuizWebsite.Domain.Users.Models.UserModel;

namespace UserService.Domain.Models
{
    public class Role
	{
		public required Guid Id { get; set; }
		public required string Name { get; set; }
		public required bool IsDefault { get; set; }                         
		public List<User> Users { get; set; }
	}
}
