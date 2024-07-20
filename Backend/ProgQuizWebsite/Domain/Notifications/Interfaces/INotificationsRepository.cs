using ProgQuizWebsite.Domain.Notifications.Models;

namespace ProgQuizWebsite.Domain.Notifications.Interfaces
{
	public interface INotificationsRepository
	{
		Task<List<Notification>> GetAllAsync();
		Task<Guid> AddAsync(Notification notification);
		Task SaveChangesAsync();
	}
}
