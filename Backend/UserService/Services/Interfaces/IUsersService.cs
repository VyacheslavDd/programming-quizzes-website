﻿using Core.Base.Service.Interfaces;
using UserService.Api.PostModels.Users;
using UserService.Api.ResponseModels;
using UserService.Api.ResponseModels.Users;
using UserService.Domain.Models;

namespace UserService.Services.Interfaces
{
	public interface IUsersService
	{
		Task<List<User>> GetAllAsync();
		Task<User> FindByGuidAsync(Guid id);
		Task<User> FindByEmailAsync(string email);
		Task<User> FindByLoginAsync(string login);
		Task<User> FindByPhoneAsync(long phone);
		Task DeleteByGuidAsync(Guid id);
		bool IsRoleAssigned(User user, Role role);
		Task<UpdateUserResponse> UpdateAsync(Guid id, User userModel);
		Task<UpdateUserPasswordResponse> UpdatePasswordAsync(Guid id, UpdatePasswordModel passwordModel);
	}
}
