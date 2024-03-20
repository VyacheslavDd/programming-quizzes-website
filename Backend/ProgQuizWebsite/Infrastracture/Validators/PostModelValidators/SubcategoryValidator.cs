using Core.Constants;
using FluentValidation;
using ProgQuizWebsite.Api.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Validators.PostModelValidators
{
    public class SubcategoryValidator : AbstractValidator<SubcategoryPostModel>
    {
        public SubcategoryValidator()
        {
            RuleFor(sc => sc.LanguageCategoryId).NotNull().NotEmpty().WithMessage("Id категории должен быть больше 0");
            RuleFor(sc => sc.Name).NotNull().NotEmpty().
                MinimumLength(DataRestrictions.SubcategoryNameMinLength).MaximumLength(DataRestrictions.SubcategoryNameMaxLength).
                WithMessage("Длина названия подкатегории не соответствует норме");
        }

    }
}
