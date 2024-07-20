using UserService.Api.ResponseModels.Roles;

namespace UserService.Api.ResponseModels.Users
{
	public class UserResponse
	{
		public required Guid Id { get; set; }
		public required string Name { get; set; }
		public required string Surname { get; set; }
		public required DateTime BirthDate { get; set; }
		public required long PhoneNumber { get; set; }
		public required string Email { get; set; }
		public required string Login { get; set; }
		public required bool ReceiveNotifications { get; set; }
		public required List<RoleResponse> Roles { get; set; }
	}
}
