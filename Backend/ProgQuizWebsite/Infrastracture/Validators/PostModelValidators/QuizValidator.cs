
using Core.CommonFunctions;
using Core.Constants;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using ProgQuizWebsite.Api.PostModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Validators.PostModelValidators
{
    public class QuizValidator : AbstractValidator<QuizPostModel>
    {
        public QuizValidator()
        {
            RuleFor(qz => qz.Title).NotNull().NotEmpty().
                MinimumLength(DataRestrictions.QuizTitleMinLength).MaximumLength(DataRestrictions.QuizTitleMaxLength).
                WithMessage("Длина названия викторины не соответствует норме");
            RuleFor(qz => qz.Description).NotNull().NotEmpty().
                MinimumLength(DataRestrictions.QuizDescriptionMinLength).MaximumLength(DataRestrictions.QuizDescriptionMaxLength).
                WithMessage("Длина описания викторины не соответствует норме");
            RuleFor(qz => qz.Difficulty).NotNull().NotEmpty().IsInEnum().WithMessage("Значение сложности не соответствует ни одной из существующих");
            RuleFor(qz => qz.LanguageCategoryId).NotNull().NotEmpty().WithMessage("Id категории должен быть больше 0");
            RuleFor(qz => qz.SubcategoriesId).NotNull().NotEmpty().
                Must(list => list.Count >= DataRestrictions.QuizSubcategoriesListMinLength && list.Count <= DataRestrictions.QizSubcategoriesListMaxLength)
                .ForEach(sub => sub.NotNull().NotEmpty().WithMessage("Id подкатегории должен быть больше 0"))
                .WithMessage("Количество подкатегорий должно быть от 1 до 3");
            RuleFor(qz => qz.QuizImage).NotNull().NotEmpty().Must(CommonUtils.BeCorrectExtension)
                .WithMessage("Выберите изображение формата png, jpg, jpeg");
        }
    }
}
