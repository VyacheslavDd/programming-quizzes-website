using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public static class SpecialConstants
    {
        public static string UnknownValue { get; } = "Unknown";
        public static string UploadsDirectoryName { get; } = "Uploads";
        public static string QuizImagesBucketName { get; } = "quiz-images";
        public static string QuestionImagesBucketName { get; } = "question-images";
        public static string UserImagesBucketName { get; } = "user-images";
        public static string SmptPasswordFileName { get; } = "SmtpPassword.txt";
        public static string ContentCountHeaderName { get; } = "Content-Count";
        public static string NotificationsQueueName { get; } = "NotificationsQueue";
        public static string JwtTokenKeyPath { get; } = "AppSettings:JwtTokenKey";

	}
}
