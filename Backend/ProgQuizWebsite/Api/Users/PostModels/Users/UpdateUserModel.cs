using ProgQuizWebsite.Api.Users.PostModels.Users;
using ProgQuizWebsite.Domain.Users.Models.UserModel;

namespace UserService.Api.PostModels.Users
{
	public class UpdateUserModel
	{
		public UserInfoPostModel UserInfo { get; set; }
		public IFormFile? Avatar { get; set; }
	}
}
