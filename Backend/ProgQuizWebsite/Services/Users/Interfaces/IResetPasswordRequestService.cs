using ProgQuizWebsite.Api.Users.PostModels.PasswordRequests;
using ProgQuizWebsite.Api.Users.ResponseModels.Users;
using ProgQuizWebsite.Domain.Users.Models.ResetPasswordRequests;

namespace ProgQuizWebsite.Services.Users.Interfaces
{
	public interface IResetPasswordRequestService
	{
		Task<GetResetPasswordRequestResponse> GetBySequenceAsync(string sequence);
		Task<RequestResetPasswordResponse> AddAsync(PasswordRequestModel passwordRequestModel);
		Task DeleteAsync(Guid userId);
		Task SaveChangesAsync();
	}
}
