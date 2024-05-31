using Core.Base.Repository;
using UserService.Domain.Models;

namespace UserService.Domain.Interfaces
{
	public interface IRoleRepository : IRepository<Role>
	{
		Task<Role?> GetDefaultRoleAsync();
	}
}
