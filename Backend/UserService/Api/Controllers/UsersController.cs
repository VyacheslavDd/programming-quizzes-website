﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.PostModels.Users;
using UserService.Api.ResponseModels.Roles;
using UserService.Api.ResponseModels.Users;
using UserService.Domain.Models;
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

		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync()
		{
			var users = await _usersService.GetAllAsync();
			var models = _mapper.Map<List<UserResponse>>(users);
			return Ok(models);
		}

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

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<IActionResult> DeleteByGuidAsync([FromRoute] Guid id)
		{
			await _usersService.DeleteByGuidAsync(id);
			return Ok();
		}

		[HttpPut]
		[Route("update/{id}")]
		public async Task<IActionResult> UpdateByGuidAsync([FromRoute] Guid id, [FromBody] UpdateUserModel updateUserModel)
		{
			var userModel = _mapper.Map<User>(updateUserModel);
			var response = await _usersService.UpdateAsync(id, userModel);
			return new JsonResult(response);
		}

		[HttpPatch]
		[Route("update/{id}/password")]
		public async Task<IActionResult> UpdatePasswordAsync([FromRoute] Guid id, [FromBody] UpdatePasswordModel updatePasswordModel)
		{
			var response = await _usersService.UpdatePasswordAsync(id, updatePasswordModel);
			return new JsonResult(response);
		}
	}
}
