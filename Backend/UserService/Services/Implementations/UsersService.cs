using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Services.Interfaces;
using Core.Enums;

namespace UserService.Services.Implementations
{
	internal class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;

		public UsersService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task DeleteByGuidAsync(Guid id)
		{
			await _userRepository.DeleteAsync(id);
			await _userRepository.SaveChangesAsync();
		}

		public async Task<User> FindByEmailAsync(string email)
		{
			return await _userRepository.FindByEmailAsync(email);
		}

		public async Task<User> FindByGuidAsync(Guid id)
		{
			return await _userRepository.GetByGuidAsync(id);
		}

		public async Task<User> FindByLoginAsync(string login)
		{
			return await _userRepository.FindByLoginAsync(login);
		}

		public async Task<List<User>> GetAllAsync()
		{
			return await _userRepository.GetAllAsync();
		}

		public bool IsRoleAssigned(User user, Role role)
		{
			foreach (var r in user.Roles)
			{
				if (r.Name == role.Name) return true;
			}
			return false;
		}
	}
}
