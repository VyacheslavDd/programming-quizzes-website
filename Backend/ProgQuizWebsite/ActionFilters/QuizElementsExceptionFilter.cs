using Data_Layer.Exceptions.QuizElementsExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ProgQuizWebsite.ActionFilters
{
	public class QuizElementsExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			var exception = context.Exception;
			var type = exception.GetType();
			if (type == typeof(ArgumentNullException))
				context.Result = new NotFoundObjectResult(exception.Message);
			else if (type == typeof(AnswersOverflowException) || type == typeof(NotUniqueException) ||
			 type == typeof(CorrectAnswersAndQuestionTypeUnmatchingException))
				context.Result = new UnprocessableEntityObjectResult(exception.Message);
			else
				context.Result = new StatusCodeResult(500);
			context.ExceptionHandled = true;
		}
	}
}
