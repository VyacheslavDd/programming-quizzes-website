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
        public static string QuizImagesDirectoryName { get; } = "QuizImages";
        public static string QuestionImagesDirectoryName { get; } = "QuestionImages";
        public static string ContentCountHeaderName { get; } = "Content-Count";
        public static string NotificationsQueueName { get; } = "NotificationsQueue";
        public static string JwtTokenKeyPath { get; } = "AppSettings:JwtTokenKey";

	}
}
