using Data_Layer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.PostModels
{
	public class QuestionPostModel
	{
		public QuestionType Type { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? SuccessInfo { get; set; }
		public string? FailureInfo { get; set; }
		public int QuizId { get; set; }
	}
}
