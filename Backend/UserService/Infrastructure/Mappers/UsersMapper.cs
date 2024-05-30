using AutoMapper;
using BCrypt.Net;
using UserService.Api.PostModels.Auth;
using UserService.Api.PostModels.Roles;
using UserService.Api.ResponseModels.Roles;
using UserService.Api.ResponseModels.Users;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Mappers
{
    public class UsersMapper : Profile
	{
		public UsersMapper()
		{
			CreateMap<User, UserResponse>();
			CreateMap<RegistrationModel, User>()
				.ForMember(user => user.PasswordHash, opt => opt.MapFrom(model =>
				BCrypt.Net.BCrypt.HashPassword(model.Password)));
			CreateMap<AuthenticationModel, User>()
				.ForMember(user => user.Email, opt => opt.MapFrom(model =>
				model.LoginOrEmail.Contains('@') ? model.LoginOrEmail : ""))
				.ForMember(user => user.Login, opt => opt.MapFrom(model =>
				model.LoginOrEmail.Contains('@') ? "" : model.LoginOrEmail));

			CreateMap<Role, RoleResponse>();
			CreateMap<RolePostModel, Role>();
			CreateMap<RoleUpdateModel, Role>();
		}
	}
}
