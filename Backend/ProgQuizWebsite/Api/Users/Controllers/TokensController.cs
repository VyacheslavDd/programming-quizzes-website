using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Services.Interfaces;

namespace ProgQuizWebsite.Api.Users.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TokensController : ControllerBase
	{
		private readonly ITokenService _tokenService;

		public TokensController(ITokenService tokenService)
		{
			_tokenService = tokenService;
		}

		/// <summary>
		/// Обновить токены пользователя, не выполняя повторную аутентификацию
		/// </summary>
		/// <param name="userId">Guid пользователя</param>
		/// <returns></returns>
		[HttpGet]
		[Route("refresh/{userId}")]
		public async Task<IActionResult> RefreshTokensAsync([FromRoute] Guid userId)
		{
			var response = await _tokenService.RefreshTokensAsync(userId);
			return new JsonResult(response);
		}

	}
}
