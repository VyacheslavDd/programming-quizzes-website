﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions.QuizElementsExceptions
{
    public class CorrectAnswersAndQuestionTypeUnmatchingException : Exception
    {
        public CorrectAnswersAndQuestionTypeUnmatchingException(string message) : base(message)
        {

        }
    }
}
