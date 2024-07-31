using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constants
{
    public static class DataRestrictions
    {
        public static int CategoryNameMinLength { get; } = 2;
        public static int CategoryNameMaxLength { get; } = 10;
        public static int SubcategoryNameMinLength { get; } = 3;
        public static int SubcategoryNameMaxLength { get; } = 15;
        public static int QuizTitleMinLength { get; } = 5;
        public static int QuizTitleMaxLength { get; } = 20;
        public static int QuizDescriptionMinLength { get; } = 10;
        public static int QuizDescriptionMaxLength { get; } = 300;
        public static int QuizSubcategoriesListMinLength { get; } = 1;
        public static int QizSubcategoriesListMaxLength { get; } = 3;
        public static int QuestionTitleMinLength { get; } = 7;
		public static int QuestionTitleMaxLength { get; } = 30;
		public static int QuestionDescriptionMinLength { get; } = 25;
		public static int QuestionDescriptionMaxLength { get; } = 150;
		public static int QuestionSuccessInfoMinLength { get; } = 10;
		public static int QuestionSuccessInfoMaxLength { get; } = 70;
		public static int QuestionFailureInfoMinLength { get; } = 10;
		public static int QuestionFailureInfoMaxLength { get; } = 70;
        public static int AnswerMinLength { get; } = 2;
        public static int AnswerMaxLength { get; } = 50;
        public static int AnswersMaxQuantity { get; } = 10;
        public static int NotificationsPerPageQuantity { get; } = 10;

        public static string[] AllowedImageExtensions = new string[]
        {
            "png", "jpg", "jpeg"
        };
	}
}
