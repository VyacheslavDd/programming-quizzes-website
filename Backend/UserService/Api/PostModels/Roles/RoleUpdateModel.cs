namespace UserService.Api.PostModels.Roles
{
	public class RoleUpdateModel
	{
		public required string Name { get; set; }
		public required bool IsDefault { get; set; } = false;
	}
}
