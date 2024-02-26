using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data_Layer.Enums;
using System.Threading.Tasks;
using Data_Layer.Constants;
using System.Net.Http.Headers;

namespace Business_Layer.Services.Implementations.MainServices
{
    public class AnswerService : BaseService<Answer>
    {
        private readonly IValidationService _validationService;

        public AnswerService(IUnitOfWork unitOfWork, IValidationService validationService) : base(unitOfWork.AnswerRepository, unitOfWork)
        {
            _validationService = validationService;
        }

        public async override Task ValidateItemData(Answer? answer)
        {
            await _validationService.ValidateAnswer(answer, _unitOfWork.QuestionRepository);
        }
    }
}
