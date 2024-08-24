using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models.UserConfirm;
using ProgQuizWebsite.Domain.Users.Models.UserModel;
using ProgQuizWebsite.Services.Users.Interfaces;

namespace ProgQuizWebsite.Services.Users.Implementations
{
	internal class ConfirmationService : IConfirmationService
	{
		private readonly IConfirmationRepository _confirmationRepository;

		public ConfirmationService(IConfirmationRepository confirmationRepository)
		{
			_confirmationRepository = confirmationRepository;
		}

		public async Task AddConfirmationAsync(User user, string sequence)
		{
			var userConfirmation = new UserConfirmation() { Sequence = sequence, User = user };
			await _confirmationRepository.AddConfirmationAsync(userConfirmation);
		}

		public async Task<UserConfirmation> GetUserConfirmationBySequenceAsync(string sequence)
		{
			return await _confirmationRepository.GetUserConfirmationBySequenceAsync(sequence);
		}

		public async Task RemoveConfirmationAsync(Guid userId)
		{
			await _confirmationRepository.RemoveConfirmationAsync(userId);
		}

		public async Task SaveChangesAsync()
		{
			await _confirmationRepository.SaveChangesAsync();
		}
	}
}
