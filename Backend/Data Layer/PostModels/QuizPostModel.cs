using Data_Layer.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.PostModels
{
    public class QuizPostModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int LanguageCategoryId { get; set; }
        public QuizDifficulty Difficulty { get; set; }
        public List<int> SubcategoriesId { get; set; }
        public IFormFile QuizImage { get; set; }
    }
}
