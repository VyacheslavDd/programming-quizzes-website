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
		private readonly IRepository<Role> _rolesRepository;

		public RolesService(IRepository<Role> rolesRepository)
		{
			_rolesRepository = rolesRepository;
		}

		public async Task DeteteAsync(Guid id)
		{
			await _rolesRepository.DeleteAsync(id);
			await _rolesRepository.SaveChangesAsync();
		}

		public async Task<List<Role?>> GetAllAsync()
		{
			return await _rolesRepository.GetAllAsync();
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
			var response = await CheckRoles(role, considerId: true);
			if (response.ResponseCode != Core.Enums.ResponseCode.Conflict) await _rolesRepository.SaveChangesAsync();
			return new RoleUpdateResponse() { ResponseCode = response.ResponseCode, ErrorMessage = response.ErrorMessage };
		}

		public async Task<BaseHttpResponse> CheckRoles(Role role, bool considerId = false)
		{
			var roles = await GetAllAsync();
			var isUnique = !roles.Any(r => r.Name.ToLower() == role.Name.ToLower() && r.Id != role.Id);
			var isNoDefaultCollision = !role.IsDefault || roles.FirstOrDefault(r => r.IsDefault && r.Id != role.Id) == null;
			if (isUnique && isNoDefaultCollision) return new BaseHttpResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
			return new BaseHttpResponse() { ResponseCode = Core.Enums.ResponseCode.Conflict,
				ErrorMessage = "Роль не уникальна или уже имеется роль по умолчанию." };
		}

		public async Task<RoleAddResponse> AddAsync(Role role)
		{
			var response = await CheckRoles(role);
			if (response.ResponseCode != Core.Enums.ResponseCode.Success) return new RoleAddResponse()
			{
				ResponseCode = response.ResponseCode,
				ErrorMessage = response.ErrorMessage
			};
			await _rolesRepository.AddAsync(role);
			await _rolesRepository.SaveChangesAsync();
			return new RoleAddResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.Created
			};
		}

		Task<Guid> IService<Role>.AddAsync(Role? item)
		{
			throw new NotImplementedException();
		}
	}
}
