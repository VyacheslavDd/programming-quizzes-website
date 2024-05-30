namespace UserService.Api.PostModels.Roles
{
	public class RolePostModel
	{
		public required string Name { get; set; }
		public required bool IsDefault { get; set; } = false;
	}
}
