using Core.Base.Service.Interfaces;
using ProgQuizWebsite.Api.Users.PostModels.Users;
using ProgQuizWebsite.Api.Users.ResponseModels.Users;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using UserService.Api.PostModels.Users;
using UserService.Api.ResponseModels;
using UserService.Api.ResponseModels.Users;
using UserService.Domain.Models;

namespace UserService.Services.Interfaces
{
    public interface IUsersService
	{
		Task<List<User>> GetAllAsync();
		Task<List<User>> GetNotificationSubscribers();
		Task<User> FindByGuidAsync(Guid id);
		Task<User> FindByEmailAsync(string email);
		Task<User> FindByLoginAsync(string login);
		Task<User> FindByPhoneAsync(long phone);
		Task DeleteByGuidAsync(Guid id);
		Task ClearNewNotificationsCountFieldAsync(Guid id);
		bool IsRoleAssigned(User user, Role role);
		Task<UpdateUserResponse> UpdateAsync(Guid id, User userModel);
		Task<UpdateUserPasswordResponse> UpdatePasswordAsync(Guid id, UpdatePasswordModel passwordModel);
		Task<UpdateUserNotificationsResponse> UpdateUserNotificationsAsync(Guid id, UpdateNotificationsModel notificationsModel);
	}
}
