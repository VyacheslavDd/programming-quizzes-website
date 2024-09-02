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
using Minio;
using Core.Constants;
using ProgQuizWebsite.Services.Users.Interfaces;
using Core.Emailing.Services;

namespace UserService.Services.Implementations
{
    internal class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;
		private readonly IImageService _imageService;
		private readonly IConfirmationService _confirmationService;
		private readonly IEmailService _emailService;
		private readonly IMinioClientFactory _minioClientFactory;

		public UsersService(IUserRepository userRepository, IImageService imageService,
			IConfirmationService confirmationService, IEmailService emailService, IMinioClientFactory minioClientFactory)
		{
			_userRepository = userRepository;
			_imageService = imageService;
			_confirmationService = confirmationService;
			_emailService = emailService;
			_minioClientFactory = minioClientFactory;
		}

		public async Task ClearNewNotificationsCountFieldAsync(Guid id)
		{
			var user = await FindByGuidAsync(id);
			if (user == null) return;
			user.UserNotificationsInfo.NewNotificationsCount = 0;
			await _userRepository.SaveChangesAsync();
		}

		public async Task<ConfirmUserResponse> ConfirmUserAsync(Guid? id)
		{
			if (id == null) return new ConfirmUserResponse()
			{
				ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Некорректная ссылка."
			};
			var guid = id.Value;
			var user = await FindByGuidAsync(guid);
			if (user == null) return new ConfirmUserResponse()
			{
				ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Аккаунта не существует"
			};
			if (user.IsConfirmed) return new ConfirmUserResponse()
			{
				ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Аккаунт уже подтвержден"
			};
			user.IsConfirmed = true;
			await _confirmationService.RemoveConfirmationAsync(guid);
			await _confirmationService.SaveChangesAsync();
			await _emailService.SendRegistrationFinishedEmailAsync(user.UserInfo.Login, user.UserInfo.Email);
			return new ConfirmUserResponse() { ResponseCode = ResponseCode.Success };
		}

		public async Task DeleteByGuidAsync(Guid id)
		{
			var minioClient = _minioClientFactory.CreateClient();
			var user = await FindByGuidAsync(id);
			if (user != null)
			{
				await _imageService.DeleteFile(minioClient, SpecialConstants.UserImagesBucketName, user.UserInfo.ImageUrl);
				await _userRepository.DeleteAsync(id);
				await _userRepository.SaveChangesAsync();
			}
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

		public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _userRepository.GetAllAsync(cancellationToken);
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

		public async Task<UpdateUserResponse> UpdateAsync(Guid id, User userModel, IFormFile avatar)
		{
			var user = await FindByGuidAsync(id);
			if (user == null) return new UpdateUserResponse() { ResponseCode = ResponseCode.NotFound,
				ErrorMessage = "Пользователь не найден" };
			var conflictingUser = (await GetAllAsync(CancellationToken.None)).Where(u => (u.UserInfo.Email == userModel.UserInfo.Email ||
			u.UserInfo.Login == userModel.UserInfo.Login ||
			u.UserInfo.PhoneNumber == userModel.UserInfo.PhoneNumber) && u.Id != id).FirstOrDefault();
			if (conflictingUser != null) return new UpdateUserResponse() { ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Неуникальный адрес почты, логин или телефон" };
			var minioClient = _minioClientFactory.CreateClient();
			if (avatar != null) await _imageService.DeleteFile(minioClient, SpecialConstants.UserImagesBucketName, user.UserInfo.ImageUrl);
			var path = avatar == null ? user.UserInfo.ImageUrl : _imageService.CreateName(avatar.FileName);
			user.UserInfo.Name = userModel.UserInfo.Name;
			user.UserInfo.Surname = userModel.UserInfo.Surname;
			user.UserInfo.Email = userModel.UserInfo.Email;
			user.UserInfo.Login = userModel.UserInfo.Login;
			user.UserInfo.PhoneNumber = userModel.UserInfo.PhoneNumber;
			user.UserInfo.BirthDate = userModel.UserInfo.BirthDate.AddDays(1).ToUniversalTime();
			user.UserInfo.ImageUrl = path;
			await _userRepository.SaveChangesAsync();
			if (avatar != null)
				await _imageService.SaveFileAsync(avatar, minioClient, SpecialConstants.UserImagesBucketName, path);
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

		public async Task<ResetPasswordResponse> ResetPasswordAsync(Guid? id, ResetPasswordModel resetPasswordModel)
		{
			if (id == null) return new ResetPasswordResponse()
			{
				ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Запрос не существует или истёк его срок действия"
			};
			var user = await FindByGuidAsync(id.Value);
			if (user == null) return new ResetPasswordResponse()
			{
				ResponseCode = ResponseCode.BadRequest,
				ErrorMessage = "Данного аккаунта не существует"
			};
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(resetPasswordModel.Password);
			await _userRepository.SaveChangesAsync();
			return new ResetPasswordResponse() { ResponseCode = ResponseCode.Success };
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
