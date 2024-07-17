using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Api.Quizzes.ViewModels
{
    public class QuizViewModel
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int Difficulty { get; set; }
        public string? CreationDate { get; set; }
        public string? CategoryName { get; set; }
        public List<SubcategoryViewModel?> Subcategories { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
