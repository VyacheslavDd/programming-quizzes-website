using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Contexts;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure.Startups
{
	public static class DomainStartup
	{
		public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration config)
		{
			services.AddDbContext<UserContext>(options => options.UseNpgsql(config.GetConnectionString("PostgresDb")),
				ServiceLifetime.Scoped);
			services.AddScoped<IUserRepository, UserRepository>();
			return services;
		}
	}
}
