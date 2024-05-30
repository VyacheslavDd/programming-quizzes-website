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

		[HttpGet]
		[Route("all")]
		public async Task<IActionResult> GetAllAsync()
		{
			var roles = await _rolesService.GetAllAsync();
			var models = _mapper.Map<List<RoleResponse>>(roles);
			return Ok(models);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
		{
			var role = await _rolesService.GetByGuidAsync(id);
			var model = _mapper.Map<RoleResponse>(role);
			return Ok(model);
		}

		[HttpDelete]
		[Route("delete/{id}")]
		public async Task<IActionResult> DeleteByIdAsync([FromRoute] Guid id)
		{
			await _rolesService.DeteteAsync(id);
			return Ok();
		}

		[HttpPost]
		[Route("create")]
		[ServiceFilter<RoleFilter>]
		public async Task<IActionResult> CreateAsync([FromBody] RolePostModel model)
		{
			var role = _mapper.Map<Role>(model);
			var response = await _rolesService.AddAsync(role);
			return new JsonResult(response);
		}

		[HttpPut]
		[Route("update/{id}")]
		[ServiceFilter<RoleFilter>]
		public async Task<IActionResult> UpdateAsync([FromBody] RoleUpdateModel model, [FromRoute] Guid id)
		{
			var role = _mapper.Map<Role>(model);
			var response = await _rolesService.UpdateAsync(role, id);
			return new JsonResult(response);
		}
	}
}
