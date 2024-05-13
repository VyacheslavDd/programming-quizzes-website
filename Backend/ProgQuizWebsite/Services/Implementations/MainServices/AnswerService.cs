﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Core.Base.Service.Implementations;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Services.Interfaces;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
using Core.Redis.Interfaces;

namespace ProgQuizWebsite.Services.Implementations.MainServices
{
    public class AnswerService : BaseService<Answer>
    {
        private readonly IValidationService _validationService;

        public AnswerService(IUnitOfWork unitOfWork, IValidationService validationService, IRedisService redisService)
            : base(unitOfWork.AnswerRepository, unitOfWork, redisService)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemDataAsync(Answer? answer)
        {
            await _validationService.ValidateAnswer(answer, _unitOfWork.QuestionRepository);
        }
    }
}
