namespace UserService.Domain.Models
{
	public class User
	{
		public required Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public long PhoneNumber { get; set; }
		public required string Email { get; set; }
		public required string Login { get; set; }
		public required string PasswordHash { get; set; }
		public List<Role> Roles { get; set; }
	}
}
