using Core.Base.Service.Interfaces;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;
using UserService.Services.Interfaces;
using Core.Enums;

namespace UserService.Services.Implementations
{
	sealed class UsersService : IUsersService
	{
		private readonly IUserRepository _userRepository;

		public UsersService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
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
	}
}
