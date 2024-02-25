using Data_Layer.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models.CategoryModels
{
    [Table("Subcategories")]
    public class QuizSubcategory
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Guid LanguageCategoryId { get; set; }
        public LanguageCategory? LanguageCategory { get; set; }
        public List<Quiz?> Quizzes { get; set; }
    }
}
