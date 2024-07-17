namespace UserService.Api.PostModels.Auth
{
    public class AuthenticationModel
    {
        public required string LoginOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
