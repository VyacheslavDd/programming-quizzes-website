namespace UserService.Api.PostModels.Users
{
	public class UpdatePasswordModel
	{
		public required string PreviousPassword { get; set; }
		public required string NewPassword { get; set; }
	}
}
