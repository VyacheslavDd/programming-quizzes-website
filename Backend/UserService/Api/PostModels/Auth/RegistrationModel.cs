namespace UserService.Api.PostModels.Auth
{
    public class RegistrationModel
    {
        public required string Email { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
