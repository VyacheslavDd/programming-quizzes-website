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
        public QuestionService(IUnitOfWork unitOfWork) : base(unitOfWork.QuestionRepository, unitOfWork)
        {
        }

        public async override Task<bool> ValidateItemData(Question? question)
        {
            var doesQuizExist = await DoesQuizExist(question.QuizId);
            if (!doesQuizExist) return false;
            var isTypeCorresponding = IsTypeCorresponding(question);
            if (!isTypeCorresponding) return false;
            return true;
        }

        private async Task<bool> DoesQuizExist(Guid quizId)
        {
            return await _unitOfWork.QuizRepository.GetByGuidAsync(quizId) is not null;
        }

        private bool IsTypeCorresponding(Question? question)
        {
            if (question.Answers is null) return true;
            if (question.Type == QuestionType.Single && question.Answers.Where(a => a.IsCorrect).Count() > 1) return false;
            return true;
        }
    }
}
