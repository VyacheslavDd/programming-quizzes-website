namespace UserService.Infrastructure.Startups
{
	public static class CorsStartup
	{
		public static IServiceCollection AddFrontCors(this IServiceCollection services, string policyName)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(policyName, policy =>
				{
					policy.WithOrigins("http://localhost:5173").AllowAnyMethod().AllowAnyHeader();
				});
			});
			return services;
		}
	}
}
