using ProgQuizWebsite.Domain.Users.Models.UserModel;
using UserService.Api.ResponseModels.Roles;

namespace UserService.Api.ResponseModels.Users
{
	public class UserResponse
	{
		public required Guid Id { get; set; }
		public required UserInfo UserInfo { get; set; }
		public required UserNotificationsInfo UserNotificationsInfo { get; set; }
		public required List<RoleResponse> Roles { get; set; }
	}
}
