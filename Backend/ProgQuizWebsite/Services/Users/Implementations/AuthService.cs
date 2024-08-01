using BCrypt.Net;
using Core.Enums;
using ProgQuizWebsite.Domain.Users.Interfaces;
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

		public AuthService(IUserRepository userRepository, IUsersService usersService, IRolesService rolesService,
			ITokenService tokenService)
		{
			_userRepository = userRepository;
			_usersService = usersService;
			_rolesService = rolesService;
			_tokenService = tokenService;
		}

		public async Task<RegistrationResponse> RegisterAsync(User user)
		{
			var doesUserExistByEmail = (await _usersService.FindByEmailAsync(user.Email)) != null;
			if (doesUserExistByEmail)
				return new RegistrationResponse() { ResponseCode = ResponseCode.BadRequest, ErrorMessage = "На указанный e-mail уже зарегистрирован аккаунт." };
			var doesUserExistByLogin = (await _usersService.FindByLoginAsync(user.Login)) != null;
			if (doesUserExistByLogin)
				return new RegistrationResponse() { ResponseCode = ResponseCode.BadRequest, ErrorMessage = "Логин уже занят." };
			var defaultRole = await _rolesService.GetDefaultRoleAsync();
			user.Roles = new List<Role>();
			if (defaultRole != null) user.Roles.Add(defaultRole);
			await _userRepository.AddAsync(user);
			await _userRepository.SaveChangesAsync();
			return new RegistrationResponse() { ResponseCode = ResponseCode.Created };
		}

		public async Task<AuthenticationResponse> AuthenticateAsync(User user, string inputPassword)
		{
			var foundUser = (await _usersService.FindByEmailAsync(user.Email)) ?? (await _usersService.FindByLoginAsync(user.Login));
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
