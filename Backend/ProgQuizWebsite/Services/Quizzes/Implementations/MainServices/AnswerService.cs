
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Core.Base.Service.Implementations;
using Core.Redis.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork;
using ProgQuizWebsite.Services.Quizzes.Implementations;
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using ProgQuizWebsite.Infrastracture.Contexts;

namespace ProgQuizWebsite.Services.Quizzes.Implementations.MainServices
{
    public class AnswerService : BaseService<Answer>
    {
        private readonly IValidationService _validationService;

        public AnswerService(IUnitOfWork unitOfWork, IValidationService validationService, IRedisService redisService, QuizAppContext quizAppContext)
            : base(unitOfWork.AnswerRepository, unitOfWork, redisService, quizAppContext)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemDataAsync(Answer? answer)
        {
            await _validationService.ValidateAnswer(answer, _unitOfWork.QuestionRepository);
        }
    }
}
