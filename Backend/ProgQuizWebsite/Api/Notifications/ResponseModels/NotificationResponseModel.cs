namespace ProgQuizWebsite.Api.Notifications.ResponseModels
{
	public class NotificationResponseModel
	{
		public required Guid Id { get; set; }
		public required string Content { get; set; }
		public required DateTime Date { get; set; }
	}
}
