
using Core.Enums;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Api.Quizzes.PostModels
{
    public class QuizPostModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid LanguageCategoryId { get; set; }
        [EnumDataType(typeof(QuizDifficulty))]
        [JsonConverter(typeof(StringEnumConverter))]
        public QuizDifficulty Difficulty { get; set; }
        public List<Guid> SubcategoriesId { get; set; }
        public IFormFile QuizImage { get; set; }
    }
}
