using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Infrastructure.Repositories;

namespace UserService.Infrastructure.Startups
{
	public static class DomainStartup
	{
		public static IServiceCollection AddUserDomain(this IServiceCollection services, IConfiguration config)
		{
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();
			return services;
		}
	}
}
