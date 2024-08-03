using Core.Base.Repository;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using UserService.Api.ResponseModels;

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
