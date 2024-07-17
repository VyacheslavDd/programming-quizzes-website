namespace UserService.Api.ResponseModels.Roles
{
	public class RoleResponse
	{
		public required Guid Id { get; set; }
		public required string Name { get; set; }
		public required bool IsDefault { get; set; }
	}
}
