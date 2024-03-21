
using Core.Base.Service.Implementations;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Infrastracture.UnitOfWork;
using ProgQuizWebsite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Implementations.MainServices
{
    public class QuestionService : BaseService<Question>
    {
        private readonly IValidationService _validationService;
        public QuestionService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.QuestionRepository, unitOfWork)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemDataAsync(Question? question)
        {
            await _validationService.ValidateQuestion(question, _unitOfWork.QuizRepository);
        }
    }
}
