using AutoMapper;
using Core.Base.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Minio;
using ProgQuizWebsite.Api.Users.PostModels.Users;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using ProgQuizWebsite.Infrastracture.Users.Filters;
using ProgQuizWebsite.Services.Users.Interfaces;
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
		private readonly IResetPasswordRequestService _resetPasswordRequestService;
		private readonly IMapper _mapper;

		public UsersController(IUsersService usersService, IResetPasswordRequestService resetPasswordRequestService, IMapper mapper)
		{
			_usersService = usersService;
			_resetPasswordRequestService = resetPasswordRequestService;
			_mapper = mapper;
		}

		/// <summary>
		/// Получить всех пользователей
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[Route("all")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
		{
			var users = await _usersService.GetAllAsync(cancellationToken);
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
		[Authorize]
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
		[Authorize]
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
		[Authorize]
		public async Task<IActionResult> UpdateByGuidAsync([FromRoute] Guid id, [FromForm] UpdateUserModel updateUserModel)
		{
			var userModel = _mapper.Map<User>(updateUserModel);
			var response = await _usersService.UpdateAsync(id, userModel, updateUserModel.Avatar);
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
		[Authorize]
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
		[Authorize]
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
		[Authorize]
		public async Task<IActionResult> ClearNewNotificationsCountFieldAsync([FromRoute] Guid id)
		{
			await _usersService.ClearNewNotificationsCountFieldAsync(id);
			return Ok();
		}

		/// <summary>
		/// Сбросить пароль
		/// </summary>
		/// <param name="sequence">Уникальная последовательность</param>
		/// <param name="id">Guid пользователя</param>
		/// <param name="resetPasswordModel">Данные формы сброса пароля</param>
		/// <returns></returns>
		[HttpPatch]
		[Route("reset-password/{id}")]
		public async Task<IActionResult> ResetPasswordAsync([FromQuery] string sequence, [FromRoute] Guid id,
			[FromBody] ResetPasswordModel resetPasswordModel)
		{
			var request = await _resetPasswordRequestService.GetBySequenceAsync(sequence);
			var response = await _usersService.ResetPasswordAsync(request.ResetPasswordRequest?.UserId, resetPasswordModel);
			if (response.ResponseCode == Core.Enums.ResponseCode.Success)
				await _resetPasswordRequestService.DeleteAsync(id);
			return new JsonResult(response);
		}
	}
}
