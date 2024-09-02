using Core.Base.Repository;
using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using ProgQuizWebsite.Infrastracture.Contexts;
using UserService.Api.ResponseModels;
using UserService.Domain.Interfaces;

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
			return await _quizAppContext.Users.Include(u => u.Roles).
				FirstOrDefaultAsync(user => user.UserInfo.Email.ToLower() == email.ToLower());
		}

		public async Task<User?> FindByLoginAsync(string login)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(user => user.UserInfo.Login == login);
		}

		public async Task<User?> FindByPhoneAsync(long phone)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).AsNoTracking().FirstOrDefaultAsync(user => user.UserInfo.PhoneNumber == phone);
		}

		public async Task<List<User?>> GetAllNotificationsSubscribers()
		{
			return await _quizAppContext.Users.Where(u => u.UserNotificationsInfo.ReceiveNotifications).ToListAsync();
		}

		public async override Task<User?> GetByGuidAsync(Guid id, CancellationToken cancellationToken = default)
		{
			return await _quizAppContext.Users.Include(u => u.Roles).Include(u => u.Notifications)
				.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
		}
	}
}
