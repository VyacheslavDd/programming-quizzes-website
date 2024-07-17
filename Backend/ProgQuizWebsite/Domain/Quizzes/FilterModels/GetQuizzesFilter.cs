using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Domain.Quizzes.FilterModels
{
    public class GetQuizzesFilter
    {
        public int Page { get; set; }
        public int Limit { get; set; }
        public Guid CategoryId { get; set; }
        public Guid SubcategoryId { get; set; }
        public QuizDifficulty Difficulty { get; set; }
    }
}
