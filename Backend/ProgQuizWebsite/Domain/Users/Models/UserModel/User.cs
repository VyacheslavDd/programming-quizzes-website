using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Domain.Users.Models;
using ProgQuizWebsite.Domain.Users.Models.UserConfirm;
using UserService.Domain.Models;

namespace ProgQuizWebsite.Domain.Users.Models.UserModel
{
    public class User
    {
        public required Guid Id { get; set; }
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public UserNotificationsInfo UserNotificationsInfo { get; set; } = new UserNotificationsInfo();
        public required string PasswordHash { get; set; }
        public bool IsConfirmed { get; set; } = false;
        public List<Role> Roles { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<QuizRating> QuizRatings { get; set; } = new List<QuizRating>();
        public RefreshToken? RefreshToken { get; set; }
        public UserConfirmation? UserConfirmation { get; set; }
    }
}
