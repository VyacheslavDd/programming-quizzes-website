using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Services.Users.Interfaces;
using UserService.Services.Interfaces;

namespace ProgQuizWebsite.Api.Users.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ConfirmationsController : ControllerBase
	{
		private readonly IConfirmationService _confirmationService;
		private readonly IUsersService _usersService;

		public ConfirmationsController(IUsersService usersService, IConfirmationService confirmationService)
		{
			_usersService = usersService;
			_confirmationService = confirmationService;
		}

		[HttpPatch]
		[Route("confirm")]
		public async Task<IActionResult> ConfirmUserAsync([FromQuery] string sequence)
		{
			var confirmation = await _confirmationService.GetUserConfirmationBySequenceAsync(sequence);
			var response = await _usersService.ConfirmUserAsync(confirmation?.UserId);
			return new JsonResult(response);
		}
	}
}
