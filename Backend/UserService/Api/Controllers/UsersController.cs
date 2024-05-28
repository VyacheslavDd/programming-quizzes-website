using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
			return Ok(model);
		}
	}
}
