using Core.Base;
using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels.Roles;
using UserService.Domain.Models;

namespace UserService.Services.Interfaces
{
	public interface IRolesService : IService<Role>
	{
		Task<RoleUpdateResponse> UpdateAsync(Role role, Guid roleId);
		Task<RoleСreateResponse> AddAsync(Role role);
		Task<BaseHttpResponse> CheckRolesAsync(Role role, bool considerId);
		Task<Role?> GetDefaultRoleAsync();
		Task<RoleAssignResponse> AssignRoleAsync(Guid roleId, Guid userId);
		Task<RoleRevokeResponse> RevokeRoleAsync(Guid roleId, Guid userId);
	}
}
