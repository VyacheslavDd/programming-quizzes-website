using Core.Enums;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Domain.Quizzes.Models.QuizModels
{
    [Table("Quizzes")]
    public class Quiz
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public QuizDifficulty Difficulty { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid LanguageCategoryId { get; set; }
        public QuizRatingsInfo QuizRatingsInfo { get; set; }
        public LanguageCategory? LanguageCategory { get; set; }
        public List<QuizSubcategory?> Subcategories { get; set; }
        public List<Question?> Questions { get; set; }
        public List<QuizRating> QuizRatings { get; set; } = new List<QuizRating>();

    }
}
