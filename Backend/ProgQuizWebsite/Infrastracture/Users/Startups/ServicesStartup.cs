﻿using ProgQuizWebsite.Infrastracture.Users.Filters;
using ProgQuizWebsite.Services.Users.Implementations;
using ProgQuizWebsite.Services.Users.Interfaces;
using UserService.Services.Implementations;
using UserService.Services.Interfaces;

namespace UserService.Infrastructure.Startups
{
	public static class ServicesStartup
	{
		public static IServiceCollection AddUserServices(this IServiceCollection services)
		{
			services.AddScoped<IConfirmationService, ConfirmationService>();
			services.AddScoped<ITokenService, TokenService>();
			services.AddScoped<IUsersService, UsersService>();
			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IRolesService, RolesService>();
			services.AddScoped<IResetPasswordRequestService, ResetPasswordRequestService>();
			services.AddScoped<SimpleAuthenticateFilter>();
			return services;
		}
	}
}
