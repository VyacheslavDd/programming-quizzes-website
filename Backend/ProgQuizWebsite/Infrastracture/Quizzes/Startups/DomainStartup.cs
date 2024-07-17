

using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Startups
{
    public static class DomainStartup
    {
        public static IServiceCollection AddQuizDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddDbContext<QuizAppContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("QuizDB"),
                m => m.MigrationsAssembly("ProgQuizWebsite")), ServiceLifetime.Scoped);
            return services;
        }
    }
}
