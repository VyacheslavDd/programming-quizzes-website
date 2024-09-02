namespace ProgQuizWebsite.Infrastracture.Middlewares
{
	public static class MiddlewaresStartup
	{
		public static IApplicationBuilder UseCancellationMiddleware(this IApplicationBuilder applicationBuilder)
		{
			applicationBuilder.UseMiddleware<CancellationMiddleware>();
			return applicationBuilder;
		}
	}
}
