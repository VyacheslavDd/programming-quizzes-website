using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Services.Interfaces;
using Core.Enums;
using UserService.Api.ResponseModels.Users;
using UserService.Api.PostModels.Users;
using BCrypt.Net;
using ProgQuizWebsite.Api.Users.ResponseModels.Users;
using ProgQuizWebsite.Api.Users.PostModels.Users;
using ProgQuizWebsite.Domain.Users.Models.UserModel;

namespace UserService.Services.Implementations
{
    internal class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;

		public UsersService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task ClearNewNotificationsCountFieldAsync(Guid id)
		{
			var user = await FindByGuidAsync(id);
			if (user == null) return;
			user.UserNotificationsInfo.NewNotificationsCount = 0;
			await _userRepository.SaveChangesAsync();
		}

		public async Task DeleteByGuidAsync(Guid id)
		{
			await _userRepository.DeleteAsync(id);
			await _userRepository.SaveChangesAsync();
		}

		public async Task<User> FindByEmailAsync(string email)
		{
			return await _userRepository.FindByEmailAsync(email);
		}

		public async Task<User> FindByGuidAsync(Guid id)
		{
			return await _userRepository.GetByGuidAsync(id);
		}

		public async Task<User> FindByLoginAsync(string login)
		{
			return await _userRepository.FindByLoginAsync(login);
		}

		public async Task<User> FindByPhoneAsync(long phone)
		{
			return await _userRepository.FindByPhoneAsync(phone);
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await _userRepository.GetAllAsync();
		}

		public async Task<List<User>> GetNotificationSubscribers()
		{
			return await _userRepository.GetAllNotificationsSubscribers();
		}

		public bool IsRoleAssigned(User user, Role role)
		{
			foreach (var r in user.Roles)
			{
				if (r.Name == role.Name) return true;
			}
			return false;
		}

		public async Task<UpdateUserResponse> UpdateAsync(Guid id, User userModel)
		{
			var user = await FindByGuidAsync(id);
			if (user == null) return new UpdateUserResponse() { ResponseCode = ResponseCode.NotFound,
				ErrorMessage = "Пользователь не найден" };
			var conflictingUser = (await GetAllAsync()).Where(u => (u.UserInfo.Email == userModel.UserInfo.Email ||
			u.UserInfo.Login == userModel.UserInfo.Login ||
			u.UserInfo.PhoneNumber == userModel.UserInfo.PhoneNumber) && u.Id != id).FirstOrDefault();
			if (conflictingUser != null) return new UpdateUserResponse() { ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Неуникальный адрес почты, логин или телефон" };
			user.UserInfo.Name = userModel.UserInfo.Name;
			user.UserInfo.Surname = userModel.UserInfo.Surname;
			user.UserInfo.Email = userModel.UserInfo.Email;
			user.UserInfo.Login = userModel.UserInfo.Login;
			user.UserInfo.PhoneNumber = userModel.UserInfo.PhoneNumber;
			user.UserInfo.BirthDate = userModel.UserInfo.BirthDate.AddDays(1).ToUniversalTime();
			await _userRepository.SaveChangesAsync();
			return new UpdateUserResponse() { ResponseCode = ResponseCode.Success };
		}

		public async Task<UpdateUserPasswordResponse> UpdatePasswordAsync(Guid id, UpdatePasswordModel passwordModel)
		{
			var user = await FindByGuidAsync(id);
			if (user == null) return new UpdateUserPasswordResponse()
			{ ResponseCode = ResponseCode.NotFound, ErrorMessage = "Пользователь не найден"};
			if (!BCrypt.Net.BCrypt.Verify(passwordModel.PreviousPassword, user.PasswordHash)) return new UpdateUserPasswordResponse()
			{ ResponseCode = ResponseCode.BadRequest, ErrorMessage = "Неправильно указан предыдущий пароль"};
			var newPasswordHash = BCrypt.Net.BCrypt.HashPassword(passwordModel.NewPassword);
			user.PasswordHash = newPasswordHash;
			await _userRepository.SaveChangesAsync();
			return new UpdateUserPasswordResponse() { ResponseCode = ResponseCode.Success };
		}

		public async Task<UpdateUserNotificationsResponse> UpdateUserNotificationsAsync(Guid id, UpdateNotificationsModel notificationsModel)
		{
			var user = await FindByGuidAsync(id);
			if (user == null) return new UpdateUserNotificationsResponse()
			{
				ResponseCode = ResponseCode.NotFound,
				ErrorMessage = "Пользователь не найден"
			};
			user.UserNotificationsInfo.ReceiveNotifications = notificationsModel.ReceiveNotifications;
			await _userRepository.SaveChangesAsync();
			return new UpdateUserNotificationsResponse() { ResponseCode = ResponseCode.Success };
		}
	}
}
