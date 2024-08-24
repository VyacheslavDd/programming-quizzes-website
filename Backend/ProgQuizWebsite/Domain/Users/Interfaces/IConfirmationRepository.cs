using ProgQuizWebsite.Domain.Users.Models.UserConfirm;

namespace ProgQuizWebsite.Domain.Users.Interfaces
{
	public interface IConfirmationRepository
	{
		Task<UserConfirmation> GetUserConfirmationBySequenceAsync(string sequence);
		Task RemoveConfirmationAsync(Guid userId);
		Task AddConfirmationAsync(UserConfirmation userConfirmation);
		Task SaveChangesAsync();
	}
}
