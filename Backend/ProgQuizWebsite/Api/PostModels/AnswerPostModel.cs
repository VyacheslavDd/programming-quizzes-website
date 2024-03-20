using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Api.PostModels
{
	public class AnswerPostModel
	{
		public string? Name { get; set; }
		public bool IsCorrect { get; set; }
		public Guid QuestionId { get; set; }
	}
}
