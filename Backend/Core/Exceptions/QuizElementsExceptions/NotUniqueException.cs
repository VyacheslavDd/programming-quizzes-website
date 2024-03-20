using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions.QuizElementsExceptions
{
	public class NotUniqueException : Exception
	{
		public NotUniqueException(string message) : base(message) { }
	}
}
