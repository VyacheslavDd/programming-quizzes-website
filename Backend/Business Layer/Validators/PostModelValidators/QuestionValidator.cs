﻿using Data_Layer.Enums;
using Data_Layer.PostModels;
using FluentValidation;
using System;
using Data_Layer.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Business_Layer.CommonFunctions;

namespace Business_Layer.Validators.PostModelValidators
{
    public class QuestionValidator : AbstractValidator<QuestionPostModel>
    {
        public QuestionValidator()
        {
            RuleFor(q => q.Type).IsInEnum().WithMessage("Укажите допустимый тип вопроса");
            RuleFor(q => q.Title).NotNull().NotEmpty()
                .Length(DataRestrictions.QuestionTitleMinLength, DataRestrictions.QuestionTitleMaxLength)
                .WithMessage("Несоответствие названия заданной длине.");
            RuleFor(q => q.Description).NotNull().NotEmpty()
                .Length(DataRestrictions.QuestionDescriptionMinLength, DataRestrictions.QuestionDescriptionMaxLength)
                .WithMessage("Несоответствие описания заданной длине.");
            RuleFor(q => q.SuccessInfo).NotNull().NotEmpty()
                .Length(DataRestrictions.QuestionSuccessInfoMinLength, DataRestrictions.QuestionSuccessInfoMaxLength)
                .WithMessage("Несоответствие описания в случае правильного ответа заданной длине.");
            RuleFor(q => q.FailureInfo).NotNull().NotEmpty()
                .Length(DataRestrictions.QuestionFailureInfoMinLength, DataRestrictions.QuestionFailureInfoMaxLength)
                .WithMessage("Несоответствие описания в случае неправильного ответа заданной длине.");
            RuleFor(q => q.Image).NotNull().NotEmpty().Must(CommonUtils.BeCorrectExtension)
                .WithMessage("Выберите изображение формата png, jpg, jpeg");
        }
    }
}
