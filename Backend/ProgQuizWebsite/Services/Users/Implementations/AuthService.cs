using BCrypt.Net;
using Core.CommonFunctions;
using Core.Emailing.Services;
using Core.Enums;
using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using ProgQuizWebsite.Services.Users.Interfaces;
using UserService.Api.ResponseModels.Auth;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Services.Interfaces;

namespace UserService.Services.Implementations
{
    internal class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUsersService _usersService;
		private readonly IRolesService _rolesService;
		private readonly ITokenService _tokenService;
		private readonly IConfirmationService _confirmationService;
		private readonly IEmailService _emailService;

		public AuthService(IUserRepository userRepository, IUsersService usersService, IRolesService rolesService,
			ITokenService tokenService, IEmailService emailService, IConfirmationService confirmationService)
		{
			_userRepository = userRepository;
			_usersService = usersService;
			_rolesService = rolesService;
			_tokenService = tokenService;
			_emailService = emailService;
			_confirmationService = confirmationService;
		}

		public async Task<RegistrationResponse> RegisterAsync(User user)
		{
			var doesUserExistByEmail = (await _usersService.FindByEmailAsync(user.UserInfo.Email)) != null;
			if (doesUserExistByEmail)
				return new RegistrationResponse() { ResponseCode = ResponseCode.BadRequest, ErrorMessage = "На указанный e-mail уже зарегистрирован аккаунт." };
			var doesUserExistByLogin = (await _usersService.FindByLoginAsync(user.UserInfo.Login)) != null;
			if (doesUserExistByLogin)
				return new RegistrationResponse() { ResponseCode = ResponseCode.BadRequest, ErrorMessage = "Логин уже занят." };
			var defaultRole = await _rolesService.GetDefaultRoleAsync();
			user.Roles = new List<Role>();
			if (defaultRole != null) user.Roles.Add(defaultRole);
			try
			{
				await _userRepository.AddAsync(user);
				var confirmSequence = CommonUtils.GenerateUniqueSequence();
				await _confirmationService.AddConfirmationAsync(user, confirmSequence);
				await _emailService.SendConfirmationEmailAsync(user.UserInfo.Login, user.UserInfo.Email, confirmSequence);
				await _userRepository.SaveChangesAsync();
			}
			catch
			{
				return new RegistrationResponse() { ResponseCode = ResponseCode.InternalServerError, ErrorMessage = "Произошла ошибка. Повторите позже" };
			}
			return new RegistrationResponse() { ResponseCode = ResponseCode.Created };
		}

		public async Task<AuthenticationResponse> AuthenticateAsync(User user, string inputPassword)
		{
			var foundUser = (await _usersService.FindByEmailAsync(user.UserInfo.Email)) ?? (await _usersService.FindByLoginAsync(user.UserInfo.Login));
			if (!foundUser.IsConfirmed) return new AuthenticationResponse()
			{ ResponseCode = ResponseCode.Forbidden, ErrorMessage = "Необходимо подтверждение аккаунта через почту" };
			var hashEquals = foundUser != null && BCrypt.Net.BCrypt.Verify(inputPassword, foundUser.PasswordHash);
			if (hashEquals)
			{
				var tokenResponse = await _tokenService.AddOrUpdateTokensAsync(foundUser);
				return new AuthenticationResponse() { ResponseCode = ResponseCode.Success, Token = tokenResponse.AccessToken };
			}
			return new AuthenticationResponse() { ResponseCode = ResponseCode.BadRequest, ErrorMessage = "Введённые данные некорректны." };
		}
	}
}
