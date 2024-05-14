using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Logging
{
	public static class LoggingStartup
	{
		public static ConfigureHostBuilder AddSerilog(this ConfigureHostBuilder host)
		{
			host.UseSerilog((context, configuration) =>
			{
				configuration.ReadFrom.Configuration(context.Configuration);
			});
			return host;
		}
	}
}
