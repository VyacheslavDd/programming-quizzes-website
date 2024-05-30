using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.PostModels.Auth;
using UserService.Domain.Models;
using UserService.Infrastructure.Filters;
using UserService.Services.Interfaces;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public AuthController(IAuthService authService, IMapper mapper)
		{
			_authService = authService;
			_mapper = mapper;
		}

		[HttpPost]
		[Route("register")]
		[ServiceFilter<AuthFilter>]
		public async Task<IActionResult> RegisterAsync([FromBody] RegistrationModel registrationModel)
		{
			var user = _mapper.Map<User>(registrationModel);
			var response = await _authService.RegisterAsync(user);
			return new JsonResult(response);
		}

		[HttpPost]
		[Route("authenticate")]
		[ServiceFilter<AuthFilter>]
		public async Task<IActionResult> AuthenticateAsync([FromBody] AuthenticationModel authenticationModel)
		{
			var user = _mapper.Map<User>(authenticationModel);
			var response = await _authService.AuthenticateAsync(user, authenticationModel.Password);
			return new JsonResult(response);
		}
	}
}
