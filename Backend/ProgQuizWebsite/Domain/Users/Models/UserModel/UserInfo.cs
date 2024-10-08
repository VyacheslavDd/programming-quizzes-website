﻿namespace ProgQuizWebsite.Domain.Users.Models.UserModel
{
    public class UserInfo
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public long PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Login { get; set; }
        public string? ImageUrl { get; set; }
    }
}
