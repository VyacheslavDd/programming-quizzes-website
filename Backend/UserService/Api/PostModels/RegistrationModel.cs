namespace UserService.Api.PostModels
{
	public class RegistrationModel
	{
		public required string Email { get; set; }
		public required string Login {  get; set; }
		public required string Password { get; set; }
	}
}
