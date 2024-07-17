
using Core.Base.Service.Implementations;
using Core.Redis.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Infrastracture.Contexts;
using ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork;
using ProgQuizWebsite.Services.Quizzes.Implementations;
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Quizzes.Implementations.MainServices
{
    public class QuestionService : BaseService<Question>
    {
        private readonly IValidationService _validationService;
        public QuestionService(IUnitOfWork unitOfWork, IValidationService validationService, IRedisService redisService, QuizAppContext quizAppContext)
            : base(unitOfWork.QuestionRepository, unitOfWork, redisService, quizAppContext)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemDataAsync(Question? question)
        {
            await _validationService.ValidateQuestion(question, _unitOfWork.QuizRepository);
        }
    }
}
