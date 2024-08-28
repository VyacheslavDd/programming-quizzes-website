using Core.Emailing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgQuizWebsite.Api.Users.PostModels.PasswordRequests;
using ProgQuizWebsite.Services.Users.Interfaces;

namespace ProgQuizWebsite.Api.Users.Controllers
{
	[Route("api/password-requests")]
	[ApiController]
	public class PasswordRequestsController : ControllerBase
	{
		private readonly IResetPasswordRequestService _resetPasswordRequestService;

		public PasswordRequestsController(IResetPasswordRequestService resetPasswordRequestService)
		{
			_resetPasswordRequestService = resetPasswordRequestService;
		}

		/// <summary>
		/// Запросить сброс пароля для аккаунта
		/// </summary>
		/// <param name="passwordRequestModel">Необходимые данные для создания запроса сброса пароля</param>
		/// <returns></returns>
		[HttpPost]
		[Route("send")]
		public async Task<IActionResult> SendResetPasswordRequestAsync([FromBody] PasswordRequestModel passwordRequestModel)
		{
			var response = await _resetPasswordRequestService.AddAsync(passwordRequestModel);
			return new JsonResult(response);
		}

		/// <summary>
		/// Получить необходимые данные для изменения пароля
		/// </summary>
		/// <param name="sequence">Уникальная последовательность</param>
		/// <returns></returns>
		[HttpGet]
		[Route("{sequence}")]
		public async Task<IActionResult> GetResetPasswordRequestAsync([FromRoute] string sequence)
		{
			var response = await _resetPasswordRequestService.GetBySequenceAsync(sequence);
			return new JsonResult(new
			{
				response.ResponseCode,
				response.ErrorMessage,
				RequestData = new
				{
					response.ResetPasswordRequest?.Sequence,
					response.ResetPasswordRequest?.UserId
				}
			});
		}
	}
}
