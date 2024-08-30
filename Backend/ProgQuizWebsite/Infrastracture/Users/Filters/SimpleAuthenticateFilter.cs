using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProgQuizWebsite.Infrastracture.Users.Filters
{
	public class SimpleAuthenticateFilter : Attribute, IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			if (!context.HttpContext.User.Identity.IsAuthenticated)
			{
				context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
			}
		}
	}
}
