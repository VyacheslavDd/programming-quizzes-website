using Data_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ViewModels
{
	public class QuestionViewModel
	{
		public Guid Id { get; set; }
		public string? Title { get; set; }
		public int QuestionType { get; set; }
		public string? Description { get; set; }
		public string? SuccessInfo { get; set; }
		public string? FailureInfo { get; set; }
		public string? ImageUrl { get; set; }
		public Guid QuizId { get; set; }
		public List<AnswerViewModel?> Answers { get; set; }
	}
}
