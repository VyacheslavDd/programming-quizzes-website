using UserService.Api.ResponseModels.Auth;
using UserService.Domain.Models;

namespace UserService.Services.Interfaces
{
    public interface IAuthService
	{
		Task<RegistrationResponse> RegisterAsync(User user);
		Task<AuthenticationResponse> AuthenticateAsync(User user, string inputPassword);
	}
}
