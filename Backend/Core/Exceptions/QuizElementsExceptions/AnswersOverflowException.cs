using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions.QuizElementsExceptions
{
    public class AnswersOverflowException : Exception
    {
        public AnswersOverflowException(string message) : base(message) { }
    }
}
