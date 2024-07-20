using Core.Base.Repository;
using UserService.Api.ResponseModels;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> FindByEmailAsync(string email);
		Task<User?> FindByLoginAsync(string login);
		Task<User?> FindByPhoneAsync(long phone);
		Task<List<User?>> GetAllNotificationsSubscribers();
	}
}
