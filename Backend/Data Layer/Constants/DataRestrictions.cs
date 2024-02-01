using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Constants
{
    public static class DataRestrictions
    {
        public static int CategoryNameMinLength { get; } = 2;
        public static int CategoryNameMaxLength { get; } = 10;
        public static int SubcategoryNameMinLength { get; } = 3;
        public static int SubcategoryNameMaxLength { get; } = 10;
        public static int QuizTitleMinLength { get; } = 5;
        public static int QuizTitleMaxLength { get; } = 20;
        public static int QuizDescriptionMinLength { get; } = 10;
        public static int QuizDescriptionMaxLength { get; } = 300;
        public static int QuizSubcategoriesListMinLength { get; } = 1;
        public static int QizSubcategoriesListMaxLength { get; } = 3;
    }
}
