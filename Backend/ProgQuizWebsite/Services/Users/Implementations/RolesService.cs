using Core.Base;
using Core.Base.Repository;
using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels.Roles;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Services.Interfaces;

namespace UserService.Services.Implementations
{
	internal class RolesService : IRolesService
	{
		private readonly IRoleRepository _rolesRepository;
		private readonly IUsersService _usersService;

		public RolesService(IRoleRepository rolesRepository, IUsersService usersService)
		{
			_rolesRepository = rolesRepository;
			_usersService = usersService;
		}

		public async Task DeteteAsync(Guid id)
		{
			await _rolesRepository.DeleteAsync(id);
			await _rolesRepository.SaveChangesAsync();
		}

		public async Task<List<Role?>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _rolesRepository.GetAllAsync(cancellationToken);
		}

		public async Task<Role?> GetByGuidAsync(Guid id)
		{
			return await _rolesRepository.GetByGuidAsync(id);
		}

		public async Task UpdateAsync(Role? role)
		{
			await _rolesRepository.SaveChangesAsync();
		}

		public async Task<RoleUpdateResponse> UpdateAsync(Role roleNewData, Guid roleId)
		{
			var role = await GetByGuidAsync(roleId);
			if (role == null) return new RoleUpdateResponse() { ResponseCode = Core.Enums.ResponseCode.NotFound,
			ErrorMessage = "Попытка обновить несуществующую роль!" };
			role.Name = roleNewData.Name;
			role.IsDefault = roleNewData.IsDefault;
			var response = await CheckRolesAsync(role, considerId: true);
			if (response.ResponseCode != Core.Enums.ResponseCode.Conflict) await _rolesRepository.SaveChangesAsync();
			return new RoleUpdateResponse() { ResponseCode = response.ResponseCode, ErrorMessage = response.ErrorMessage };
		}

		public async Task<BaseHttpResponse> CheckRolesAsync(Role role, bool considerId = false)
		{
			var roles = await GetAllAsync();
			var isUnique = !roles.Any(r => r.Name.ToLower() == role.Name.ToLower() && r.Id != role.Id);
			var isNoDefaultCollision = !role.IsDefault || roles.FirstOrDefault(r => r.IsDefault && r.Id != role.Id) == null;
			if (isUnique && isNoDefaultCollision) return new BaseHttpResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
			return new BaseHttpResponse() { ResponseCode = Core.Enums.ResponseCode.Conflict,
				ErrorMessage = "Роль не уникальна или уже имеется роль по умолчанию." };
		}

		public async Task<RoleСreateResponse> AddAsync(Role role)
		{
			var response = await CheckRolesAsync(role);
			if (response.ResponseCode != Core.Enums.ResponseCode.Success) return new RoleСreateResponse()
			{
				ResponseCode = response.ResponseCode,
				ErrorMessage = response.ErrorMessage
			};
			await _rolesRepository.AddAsync(role);
			await _rolesRepository.SaveChangesAsync();
			return new RoleСreateResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.Created
			};
		}

		Task<Guid> IService<Role>.AddAsync(Role? item)
		{
			throw new NotImplementedException();
		}

		public async Task<Role?> GetDefaultRoleAsync()
		{
			return await _rolesRepository.GetDefaultRoleAsync();
		}

		public async Task<RoleAssignResponse> AssignRoleAsync(Guid roleId, Guid userId)
		{
			var role = await _rolesRepository.GetByGuidAsync(roleId);
			if (role == null) return new RoleAssignResponse() { ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указана несуществующая роль" };
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new RoleAssignResponse() { ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь" };
			if (_usersService.IsRoleAssigned(user, role)) return new RoleAssignResponse() { ResponseCode = Core.Enums.ResponseCode.Conflict,
				ErrorMessage = "Попытка присвоить уже добавленную роль"};
			user.Roles.Add(role);
			await _rolesRepository.SaveChangesAsync();
			return new RoleAssignResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}

		public async Task<RoleRevokeResponse> RevokeRoleAsync(Guid roleId, Guid userId)
		{
			var role = await _rolesRepository.GetByGuidAsync(roleId);
			if (role == null) return new RoleRevokeResponse() { ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указана несуществующая роль" };
			var user = await _usersService.FindByGuidAsync(userId);
			if (user == null) return new RoleRevokeResponse() { ResponseCode = Core.Enums.ResponseCode.NotFound,
				ErrorMessage = "Указан несуществующий пользователь" };
			if (!_usersService.IsRoleAssigned(user, role)) return new RoleRevokeResponse() { ResponseCode = Core.Enums.ResponseCode.Conflict,
				ErrorMessage = "Попытка удалить не добавленную пользователю роль"};
			user.Roles.Remove(role);
			await _rolesRepository.SaveChangesAsync();
			return new RoleRevokeResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}
	}
}
