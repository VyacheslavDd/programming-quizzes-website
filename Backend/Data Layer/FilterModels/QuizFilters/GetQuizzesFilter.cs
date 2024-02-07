using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.FilterModels.QuizFilters
{
	public class GetQuizzesFilter
	{
		public int Page { get; set; }
		public int Limit { get; set; }
	}
}
