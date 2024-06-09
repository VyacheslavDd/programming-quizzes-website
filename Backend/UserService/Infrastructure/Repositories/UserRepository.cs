using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Contexts;

namespace UserService.Infrastructure.Repositories
{
	internal class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly UserContext _userContext;

		public UserRepository(UserContext userContext) : base(userContext, userContext.Users)
		{
			_userContext = userContext;
		}

		public async Task<User?> FindByEmailAsync(string email)
		{
			return await _userContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
		}

		public async Task<User?> FindByLoginAsync(string login)
		{
			return await _userContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(user => user.Login == login);
		}

		public async override Task<User?> GetByGuidAsync(Guid id)
		{
			return await _userContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == id);
		}
	}
}
