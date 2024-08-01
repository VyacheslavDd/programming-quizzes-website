using Core.Base;

namespace ProgQuizWebsite.Api.Users.ResponseModels.RefreshTokens
{
	public class AddOrUpdateTokensResponse : BaseHttpResponse
	{
		public required string AccessToken { get; set; }
	}
}
