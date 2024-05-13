

using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Infrastracture;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Startups
{
	public static class DomainStartup
	{
		public static IServiceCollection AddDomain(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<UnitOfWork.IUnitOfWork, UnitOfWork.UnitOfWork>();
			services.AddDbContext<QuizAppContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("QuizDB"),
				m => m.MigrationsAssembly("ProgQuizWebsite")), ServiceLifetime.Scoped);
			return services;
		}
	}
}
