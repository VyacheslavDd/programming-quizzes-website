using Core.Emailing.Options;
using Core.Emailing.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Emailing.Startup
{
	public static class EmailingStartup
	{
		public static IServiceCollection AddEmailing(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<MailOptions>(configuration.GetSection("MailOptions"));
			services.AddScoped<IEmailService, EmailService>();
			return services;
		}
	}
}
