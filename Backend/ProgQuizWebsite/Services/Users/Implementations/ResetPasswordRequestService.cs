using Core.CommonFunctions;
using Core.Emailing.Services;
using ProgQuizWebsite.Api.Users.PostModels.PasswordRequests;
using ProgQuizWebsite.Api.Users.ResponseModels.Users;
using ProgQuizWebsite.Domain.Users.Interfaces;
using ProgQuizWebsite.Domain.Users.Models.ResetPasswordRequests;
using ProgQuizWebsite.Services.Users.Interfaces;
using UserService.Services.Interfaces;

namespace ProgQuizWebsite.Services.Users.Implementations
{
	public class ResetPasswordRequestService : IResetPasswordRequestService
	{
		private readonly IResetPasswordRequestRepository _resetPasswordRequestRepository;
		private readonly IUsersService _usersService;
		private readonly IEmailService _emailService;

		public ResetPasswordRequestService(IResetPasswordRequestRepository resetPasswordRequestRepository, IUsersService usersService,
			IEmailService emailService)
		{
			_resetPasswordRequestRepository = resetPasswordRequestRepository;
			_usersService = usersService;
			_emailService = emailService;
		}

		public async Task<RequestResetPasswordResponse> AddAsync(PasswordRequestModel passwordRequestModel)
		{
			var user = await _usersService.FindByEmailAsync(passwordRequestModel.Email);
			if (user == null || !user.IsConfirmed) return new RequestResetPasswordResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.BadRequest,
				ErrorMessage = "Несуществующий или неподтвержденный аккаунт"
			};
			await DeleteAsync(user.Id);
			var sequence = CommonUtils.GenerateUniqueSequence();
			var request = new ResetPasswordRequest()
			{
				Sequence = sequence,
				ExpirationDate = DateTime.UtcNow.AddMinutes(10),
				UserId = user.Id
			};
			await _resetPasswordRequestRepository.AddAsync(request);
			await SaveChangesAsync();
			await _emailService.SendResetPasswordEmailAsync(user.UserInfo.Login, user.UserInfo.Email, sequence);
			return new RequestResetPasswordResponse() { ResponseCode = Core.Enums.ResponseCode.Success };
		}

		public async Task DeleteAsync(Guid userId)
		{
			await _resetPasswordRequestRepository.DeleteAsync(userId);
			await SaveChangesAsync();
		}

		public async Task<GetResetPasswordRequestResponse> GetBySequenceAsync(string sequence)
		{
			var request = await _resetPasswordRequestRepository.GetBySequenceAsync(sequence);
			if (request == null) return new GetResetPasswordRequestResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.BadRequest,
				ErrorMessage = "Некорректная ссылка"
			};
			if (request.ExpirationDate < DateTime.UtcNow) return new GetResetPasswordRequestResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.BadRequest,
				ErrorMessage = "Срок действия ссылки истек"
			};
			return new GetResetPasswordRequestResponse()
			{
				ResponseCode = Core.Enums.ResponseCode.Success,
				ResetPasswordRequest = request
			};
		}

		public async Task SaveChangesAsync()
		{
			await _resetPasswordRequestRepository.SaveChangesAsync();
		}
	}
}
