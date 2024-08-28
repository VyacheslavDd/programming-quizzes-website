using ProgQuizWebsite.Domain.Users.Models.ResetPasswordRequests;

namespace ProgQuizWebsite.Domain.Users.Interfaces
{
	public interface IResetPasswordRequestRepository
	{
		Task<ResetPasswordRequest?> GetBySequenceAsync(string sequence);
		Task AddAsync(ResetPasswordRequest request);
		Task DeleteAsync(Guid userId);
		Task SaveChangesAsync();
	}
}
