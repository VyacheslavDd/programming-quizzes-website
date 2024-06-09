using Core.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace UserService.Infrastructure.Startups
{
	public static class AuthenticationStartup
	{
		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration
						.GetSection(SpecialConstants.JwtTokenKeyPath).Value)),
						ValidateLifetime = true,
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
			return services;
		}
	}
}
