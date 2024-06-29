using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Services.Interfaces;
using Core.Enums;
using UserService.Api.ResponseModels.Users;
using UserService.Api.PostModels.Users;
using BCrypt.Net;

namespace UserService.Services.Implementations
{
	internal class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;

		public UsersService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
			var conflictingUser = (await GetAllAsync()).Where(u => (u.Email == userModel.Email || u.Login == userModel.Login ||
			u.PhoneNumber == userModel.PhoneNumber) && u.Id != id).FirstOrDefault();
			if (conflictingUser != null) return new UpdateUserResponse() { ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Неуникальный адрес почты, логин или телефон" };
			user.Name = userModel.Name;
			user.Surname = userModel.Surname;
			user.Email = userModel.Email;
			user.Login = userModel.Login;
			user.PhoneNumber = userModel.PhoneNumber;
			user.BirthDate = userModel.BirthDate.ToUniversalTime();
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
	}
}
