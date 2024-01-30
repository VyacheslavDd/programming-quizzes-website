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
            RuleFor(sc => sc.LanguageCategoryId).NotEmpty().GreaterThan(0);
            RuleFor(sc => sc.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(20);
        }

    }
}
