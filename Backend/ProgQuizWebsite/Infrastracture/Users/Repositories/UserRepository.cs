using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Infrastracture.Contexts;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;
using UserService.Domain.Models;

namespace UserService.Infrastructure.Repositories
{
	internal class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly QuizAppContext _quizAppContext;

		public UserRepository(QuizAppContext quizAppContext) : base(quizAppContext, quizAppContext.Users)
		{
			_quizAppContext = quizAppContext;
		}

		public async Task<User?> FindByEmailAsync(string email)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).AsNoTracking().
				FirstOrDefaultAsync(user => user.Email.ToLower() == email.ToLower());
		}

		public async Task<User?> FindByLoginAsync(string login)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).AsNoTracking().FirstOrDefaultAsync(user => user.Login == login);
		}

		public async Task<User?> FindByPhoneAsync(long phone)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).AsNoTracking().FirstOrDefaultAsync(user => user.PhoneNumber == phone);
		}

		public async Task<List<User?>> GetAllNotificationsSubscribers()
		{
			return await _quizAppContext.Users.Where(u => u.ReceiveNotifications).ToListAsync();
		}

		public async override Task<User?> GetByGuidAsync(Guid id)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).Include(u => u.Notifications).FirstOrDefaultAsync(u => u.Id == id);
		}
	}
}
