using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ViewModels
{
	public class QuestionViewModel
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? QuestionType { get; set; }
		public string? Description { get; set; }
		public string? SuccessInfo { get; set; }
		public string? FailureInfo { get; set; }
		public int QuizId { get; set; }
		public List<AnswerViewModel?> Answers { get; set; }
	}
}
