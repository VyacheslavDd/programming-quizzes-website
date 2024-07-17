using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.PostModels.Roles;
using UserService.Api.ResponseModels.Roles;
using UserService.Domain.Models;
using UserService.Infrastructure.Filters;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IRolesService _rolesService;

		public RolesController(IMapper mapper, IRolesService rolesService)
		{
			_mapper = mapper;
			_rolesService = rolesService;
		}

		/// <summary>
		/// Получить все роли на сайте
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync()
		{
			var roles = await _rolesService.GetAllAsync();
			var models = _mapper.Map<List<RoleResponse>>(roles);
			return Ok(models);
		}
		/// <summary>
		/// Получить роль по Guid
		/// </summary>
		/// <param name="id">Guid роли</param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{
			var role = await _rolesService.GetByGuidAsync(id);
			var model = _mapper.Map<RoleResponse>(role);
			return Ok(model);
		}
		/// <summary>
		/// Удалить роль по Guid
		/// </summary>
		/// <param name="id">Guid роли</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
		{
			await _rolesService.DeteteAsync(id);
			return Ok();
		}

		/// <summary>
		/// Создать роль
		/// </summary>
		/// <param name="model">Данные роли</param>
		/// <returns></returns>
		[HttpPost]
		[Route("create")]
		[ServiceFilter(typeof(RoleFilter))]
		public async Task<IActionResult> CreateAsync([FromBody] RolePostModel model)
		{
			var role = _mapper.Map<Role>(model);
			var response = await _rolesService.AddAsync(role);
			return new JsonResult(response);
		}

		/// <summary>
		/// Обновить роль по Guid
		/// </summary>
		/// <param name="model">Новые данные</param>
		/// <param name="id">Guid роли</param>
		/// <returns></returns>
		[HttpPut]
		[Route("update/{id}")]
		[ServiceFilter(typeof(RoleFilter))]
		public async Task<IActionResult> UpdateAsync([FromBody] RoleUpdateModel model, [FromRoute] Guid id)
		{
			var role = _mapper.Map<Role>(model);
			var response = await _rolesService.UpdateAsync(role, id);
			return new JsonResult(response);
		}

		/// <summary>
		/// Назначить роль пользователю
		/// </summary>
		/// <param name="roleId">Guid роли</param>
		/// <param name="userId">Guid пользователя</param>
		/// <returns></returns>
		[HttpPost]
		[Route("{roleId}/assign/{userId}")]
		[ServiceFilter(typeof(RoleFilter))]
		public async Task<IActionResult> AssignRoleAsync([FromRoute] Guid roleId, [FromRoute] Guid userId)
		{
			var response = await _rolesService.AssignRoleAsync(roleId, userId);
			return new JsonResult(response);
		}

		/// <summary>
		/// Отозвать роль у пользователя
		/// </summary>
		/// <param name="roleId">Guid роли</param>
		/// <param name="userId">Guid пользователя</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("{roleId}/revoke/{userId}")]
		[ServiceFilter(typeof(RoleFilter))]
		public async Task<IActionResult> RevokeRoleAsync([FromRoute] Guid roleId, [FromRoute] Guid userId)
		{
			var response = await _rolesService.RevokeRoleAsync(roleId, userId);
			return new JsonResult(response);
		}
	}
}
