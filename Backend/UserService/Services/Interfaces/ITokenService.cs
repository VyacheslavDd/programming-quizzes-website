using UserService.Domain.Models;

namespace UserService.Services.Interfaces
{
	public interface ITokenService
	{
		string CreateToken(User user);
	}
}
