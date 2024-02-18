using Data_Layer.PostModels;
using Data_Layer.Constants;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Validators.PostModelValidators
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
