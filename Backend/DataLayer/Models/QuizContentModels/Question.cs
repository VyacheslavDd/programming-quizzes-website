using Data_Layer.Enums;
using Data_Layer.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Models.QuizContentModels
{
    [Table("Questions")]
    public class Question
    {
        public Guid Id { get; set; }
        public QuestionType Type { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? SuccessInfo { get; set; }
        public string? FailureInfo { get; set; }
        public string? ImageUrl { get; set; }
        public Guid QuizId { get; set; }
        public Quiz? Quiz { get; set; }
        public List<Answer?> Answers { get; set; }
    }
}
