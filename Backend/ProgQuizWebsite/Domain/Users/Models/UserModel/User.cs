using ProgQuizWebsite.Domain.Notifications.Models;
using ProgQuizWebsite.Domain.Users.Models;
using UserService.Domain.Models;

namespace ProgQuizWebsite.Domain.Users.Models.UserModel
{
    public class User
    {
        public required Guid Id { get; set; }
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public UserNotificationsInfo UserNotificationsInfo { get; set; } = new UserNotificationsInfo();
        public required string PasswordHash { get; set; }
        public List<Role> Roles { get; set; }
        public List<Notification> Notifications { get; set; }
        public RefreshToken? RefreshToken { get; set; }
    }
}
