using Data_Layer.Constants;
using Data_Layer.PostModels;
using Data_Layer.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryPostModel>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().
                MinimumLength(DataRestrictions.CategoryNameMinLength).MaximumLength(DataRestrictions.CategoryNameMaxLength).
                WithMessage("Введено некорректное название категории");
        }
    }
}
