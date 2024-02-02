using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.ViewModels
{
	public class AnswerViewModel
	{
		public int Id { get; set; }
		public string? Name { get; set; }
		public bool IsCorrect { get; set; }
		public string? QuestionTitle { get; set; }
	}
}
