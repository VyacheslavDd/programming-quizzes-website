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

        public AnswerService(IUnitOfWork unitOfWork) : base(unitOfWork.AnswerRepository, unitOfWork)
        {
        }

        public async override Task<bool> ValidateItemData(Answer? answer)
        {
            var doesQuestionExist = await DoesQuestionExist(answer.QuestionId);
            if (!doesQuestionExist) return false;
            var isCorresponding = await CorrespondingToQuestionRestrictions(answer.QuestionId, answer.IsCorrect);
            if (!isCorresponding) return false;
            return true;
        }

        private async Task<bool> DoesQuestionExist(Guid questionId)
        {
            return await _unitOfWork.QuestionRepository.GetByGuidAsync(questionId) is not null;
        }

        private async Task<bool> CorrespondingToQuestionRestrictions(Guid questionId, bool isCorrectAnswer)
        {
            var question = await _unitOfWork.QuestionRepository.GetByGuidAsync(questionId);
            var correctAnswersQuantity = question.Answers.Where(a => a.IsCorrect).Count();
            if (isCorrectAnswer && correctAnswersQuantity > 0 && question.Type == QuestionType.Single) return false;
            if (correctAnswersQuantity + 1 > DataRestrictions.AnswersMaxQuantity) return false;
            return true;
        }
    }
}
