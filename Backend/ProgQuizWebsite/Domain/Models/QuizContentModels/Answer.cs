using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Domain.QuizContentModels
{
    [Table("Answers")]
    public class Answer
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
        public Question? Question { get; set; }
    }
}
