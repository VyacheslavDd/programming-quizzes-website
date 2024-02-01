using Data_Layer.Constants;
using Data_Layer.PostModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Validators
{
    public class SubcategoryValidator : AbstractValidator<SubcategoryPostModel>
    {
        public SubcategoryValidator()
        {
            RuleFor(sc => sc.LanguageCategoryId).NotNull().NotEmpty().GreaterThan(0).WithMessage("Id категории должен быть больше 0");
            RuleFor(sc => sc.Name).NotNull().NotEmpty().
                MinimumLength(DataRestrictions.SubcategoryNameMinLength).MaximumLength(DataRestrictions.SubcategoryNameMaxLength).
                WithMessage("Название подкатегории должно быть от 3 до 10 символов!");
        }

    }
}
