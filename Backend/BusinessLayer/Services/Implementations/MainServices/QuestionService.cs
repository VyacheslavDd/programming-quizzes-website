using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Enums;

namespace Business_Layer.Services.Implementations.MainServices
{
    public class QuestionService : BaseService<Question>
    {
        private readonly IValidationService _validationService;
        public QuestionService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.QuestionRepository, unitOfWork)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemData(Question? question)
        {
            await _validationService.ValidateQuestion(question, _unitOfWork.QuizRepository);
        }
    }
}
