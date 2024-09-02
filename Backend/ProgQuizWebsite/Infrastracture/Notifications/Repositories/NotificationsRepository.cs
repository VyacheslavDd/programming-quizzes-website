using Microsoft.EntityFrameworkCore;
using ProgQuizWebsite.Domain.Notifications.Interfaces;
using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Infrastracture.Notifications.Repositories
{
	internal class NotificationsRepository : INotificationsRepository
	{
		private readonly QuizAppContext _context;

		public NotificationsRepository(QuizAppContext context) {
			_context = context;
		}

		public async Task<Guid> AddAsync(Notification notification)
		{
			var res = await _context.Notifications.AddAsync(notification);
			await SaveChangesAsync();
			return res.Entity.Id;
		}

		public async Task<List<Notification>> GetAllAsync(CancellationToken cancellationToken = default)
		{
			return await _context.Notifications.AsNoTracking().ToListAsync(cancellationToken);
		}

		public async Task<Notification> GetByGuidAsync(Guid id)
		{
			return await _context.Notifications.FindAsync(id);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
