using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.Users.PostModels.Users;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using UserService.Api.PostModels.Users;
using UserService.Api.ResponseModels.Roles;
using UserService.Api.ResponseModels.Users;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;
		private readonly IMapper _mapper;

		public UsersController(IUsersService usersService, IMapper mapper)
		{
			_usersService = usersService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync()
		{
			var users = await _usersService.GetAllAsync();
			var models = _mapper.Map<List<UserResponse>>(users);
			return Ok(models);
		}

		/// <summary>
		/// Получить пользователя по Guid
		/// </summary>
		/// <param name="id">Guid пользователя</param>
		/// <returns></returns>
		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetByGuidAsync([FromRoute] Guid id)
		{
			var user = await _usersService.FindByGuidAsync(id);
			if (user == null) return NoContent();
			var model = _mapper.Map<UserResponse>(user);
			var roles = _mapper.Map<List<RoleResponse>>(user.Roles);
			model.Roles = roles;
			return Ok(model);
		}

		/// <summary>
		/// Удалить пользователя по Guid
		/// </summary>
		/// <param name="id">Guid пользователя</param>
		/// <returns></returns>
		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<IActionResult> DeleteByGuidAsync([FromRoute] Guid id)
		{
			await _usersService.DeleteByGuidAsync(id);
			return Ok();
		}

		/// <summary>
		/// Обновить данные пользователя по Guid
		/// </summary>
		/// <param name="id">Guid пользователя</param>
		/// <param name="updateUserModel">Новые данные</param>
		/// <returns></returns>
		[HttpPut]
		[Route("update/{id}")]
		public async Task<IActionResult> UpdateByGuidAsync([FromRoute] Guid id, [FromBody] UpdateUserModel updateUserModel)
		{
			var userModel = _mapper.Map<User>(updateUserModel);
			var response = await _usersService.UpdateAsync(id, userModel);
			return new JsonResult(response);
		}

		/// <summary>
		/// Обновить пароль пользователя по Guid
		/// </summary>
		/// <param name="id">Guid пользователя</param>
		/// <param name="updatePasswordModel">Старый и новый пароли</param>
		/// <returns></returns>
		[HttpPatch]
		[Route("update/{id}/password")]
		public async Task<IActionResult> UpdatePasswordAsync([FromRoute] Guid id, [FromBody] UpdatePasswordModel updatePasswordModel)
		{
			var response = await _usersService.UpdatePasswordAsync(id, updatePasswordModel);
			return new JsonResult(response);
		}

		/// <summary>
		/// Обновлении опции получения уведомлений
		/// </summary>
		/// <param name="id">Guid пользователя</param>
		/// <param name="updateNotificationsModel">Новое значение опции</param>
		/// <returns></returns>
		[HttpPatch]
		[Route("update/{id}/notifications")]
		public async Task<IActionResult> UpdateNotificationsOptionAsync([FromRoute] Guid id,
			[FromBody] UpdateNotificationsModel updateNotificationsModel)
		{
			var response = await _usersService.UpdateUserNotificationsAsync(id, updateNotificationsModel);
			return new JsonResult(response);
		}

		/// <summary>
		/// Обнулить количество непрочитанных уведомлений пользователя
		/// </summary>
		/// <param name="id">Guid пользователя</param>
		/// <returns></returns>
		[HttpPatch]
		[Route("clear/{id}/notifications-count")]
		public async Task<IActionResult> ClearNewNotificationsCountFieldAsync([FromRoute] Guid id)
		{
			await _usersService.ClearNewNotificationsCountFieldAsync(id);
			return Ok();
		}
	}
}
