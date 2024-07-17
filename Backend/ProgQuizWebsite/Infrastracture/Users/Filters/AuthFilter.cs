using Core.Base;
using Core.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace UserService.Infrastructure.Filters
{
	public class AuthFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			context.Result = new JsonResult(new BaseHttpResponse() { ResponseCode = ResponseCode.InternalServerError,
				ErrorMessage = "Ошибка сервера. Попробуйте позже." });
			context.ExceptionHandled = true;
		}
	}
}
