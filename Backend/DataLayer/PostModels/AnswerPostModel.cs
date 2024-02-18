using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.PostModels
{
	public class AnswerPostModel
	{
		public string? Name { get; set; }
		public bool IsCorrect { get; set; }
		public int QuestionId { get; set; }
	}
}
