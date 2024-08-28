using Core.Base;
using ProgQuizWebsite.Domain.Users.Models.ResetPasswordRequests;

namespace ProgQuizWebsite.Api.Users.ResponseModels.Users
{
	public class GetResetPasswordRequestResponse : BaseHttpResponse
	{
		public ResetPasswordRequest? ResetPasswordRequest { get; set; }
	}
}
