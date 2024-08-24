using ProgQuizWebsite.Domain.Users.Models.UserConfirm;
using ProgQuizWebsite.Domain.Users.Models.UserModel;

namespace ProgQuizWebsite.Services.Users.Interfaces
{
	public interface IConfirmationService
	{
		Task<UserConfirmation> GetUserConfirmationBySequenceAsync(string sequence);
		Task AddConfirmationAsync(User user, string sequence);
		Task RemoveConfirmationAsync(Guid userId);
		Task SaveChangesAsync();
	}
}
