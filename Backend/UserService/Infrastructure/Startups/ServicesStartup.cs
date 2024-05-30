using UserService.Services.Implementations;
using UserService.Services.Interfaces;

namespace UserService.Infrastructure.Startups
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IRolesService, RolesService>();
			return services;
		}
	}
}
