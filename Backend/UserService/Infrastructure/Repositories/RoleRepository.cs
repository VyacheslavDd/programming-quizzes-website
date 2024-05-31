using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Contexts;

namespace UserService.Infrastructure.Repositories
{
	internal class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		private readonly UserContext _userContext;

		public RoleRepository(UserContext userContext) : base(userContext, userContext.Roles)
		{
			_userContext = userContext;
		}

		public async Task<Role?> GetDefaultRoleAsync()
		{
			return await _userContext.Roles.FirstOrDefaultAsync(r => r.IsDefault);
		}
	}
}
