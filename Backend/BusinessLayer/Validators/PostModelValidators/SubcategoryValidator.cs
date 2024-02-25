using Data_Layer.Constants;
using Data_Layer.PostModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Validators.PostModelValidators
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
