using Serilog;

namespace ProgQuizWebsite.Infrastracture.Middlewares
{
	public class CancellationMiddleware
	{
		private readonly Serilog.ILogger _logger;
		private readonly RequestDelegate _next;

		public CancellationMiddleware(RequestDelegate next, Serilog.ILogger logger)
		{
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception e) when (e is OperationCanceledException)
			{
				_logger.Error($"{httpContext.Request.Path}: запрос отменён");
			}
		}
	}
}
