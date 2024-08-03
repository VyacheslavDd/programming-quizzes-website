using ProgQuizWebsite.Domain.Users.Models.UserModel;
using UserService.Api.ResponseModels.Auth;

namespace UserService.Services.Interfaces
{
    public interface IAuthService
	{
		Task<RegistrationResponse> RegisterAsync(User user);
		Task<AuthenticationResponse> AuthenticateAsync(User user, string inputPassword);
	}
}
