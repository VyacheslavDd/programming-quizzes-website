using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Api.ViewModels
{
	public class AnswerViewModel
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public bool IsCorrect { get; set; }
		public string? QuestionTitle { get; set; }
	}
}
