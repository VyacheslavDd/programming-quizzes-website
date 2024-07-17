
using Core.Constants;
using FluentValidation;
using ProgQuizWebsite.Api.Quizzes.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Quizzes.Validators.PostModelValidators
{
    public class AnswerValidator : AbstractValidator<AnswerPostModel>
    {
        public AnswerValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty()
                .Length(DataRestrictions.AnswerMinLength, DataRestrictions.AnswerMaxLength)
                .WithMessage("Ответ не соответствует заданной длине.");
        }
    }
}
