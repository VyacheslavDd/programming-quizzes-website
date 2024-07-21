using ProgQuizWebsite.Domain.Notifications.Models;

namespace ProgQuizWebsite.Domain.Notifications.Interfaces
{
	public interface INotificationsRepository
	{
		Task<List<Notification>> GetAllAsync();
		Task<Notification> GetByGuidAsync(Guid id);
		Task<Guid> AddAsync(Notification notification);
		Task SaveChangesAsync();
	}
}
