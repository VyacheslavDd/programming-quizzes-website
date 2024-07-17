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
    public class CategoryValidator : AbstractValidator<CategoryPostModel>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().
                MinimumLength(DataRestrictions.CategoryNameMinLength).MaximumLength(DataRestrictions.CategoryNameMaxLength).
                WithMessage("Длина названия категории не соответствует норме");
        }
    }
}
