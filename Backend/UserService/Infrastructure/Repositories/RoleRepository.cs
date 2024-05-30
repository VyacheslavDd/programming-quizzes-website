using Core.Base.Repository;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Contexts;

namespace UserService.Infrastructure.Repositories
{
	internal class RoleRepository : BaseRepository<Role>
	{
		private readonly UserContext _userContext;

		public RoleRepository(UserContext userContext) : base(userContext, userContext.Roles)
		{
			_userContext = userContext;
		}
	}
}
