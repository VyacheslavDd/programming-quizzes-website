namespace ProgQuizWebsite.Api.Users.PostModels.Users
{
	public class UserInfoPostModel
	{
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public DateTime BirthDate { get; set; }
		public long PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Login { get; set; }
	}
}
