using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels;
using UserService.Domain.Models;

namespace UserService.Services.Interfaces
{
	public interface IUsersService
	{
		Task<List<User>> GetAllAsync();
		Task<User> FindByGuidAsync(Guid id);
		Task<User> FindByEmailAsync(string email);
		Task<User> FindByLoginAsync(string login);
	}
}
