using Core.Enums;

namespace UserService.Api.ResponseModels.Auth
{
    public class RegistrationResponse
    {
        public required ResponseCode ResponseCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
