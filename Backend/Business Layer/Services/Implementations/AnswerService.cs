﻿using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data_Layer.Enums;
using System.Threading.Tasks;
using Data_Layer.Constants;

namespace Business_Layer.Services.Implementations
{
	public class AnswerService : IService<Answer>
	{
		private readonly IUnitOfWork _unitOfWork;

		public AnswerService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> AddAsync(Answer? answer)
		{
			var doesQuestionExist = await DoesQuestionExist(answer.QuestionId);
			if (!doesQuestionExist) return false;
			var isCorresponding = await CorrespondingToQuestionRestrictions(answer.QuestionId, answer.IsCorrect);
			if (!isCorresponding) return false;
			var isUnique = await IsUnique(answer.QuestionId, answer.Name);
			if (!isUnique) return false;
			try
			{
				await _unitOfWork.AnswerRepository.AddAsync(answer);
				await _unitOfWork.Save();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<Answer?>> GetAllAsync()
		{
			return await _unitOfWork.AnswerRepository.GetAllAsync();
		}

		public async Task<Answer?> GetByIdAsync(int id)
		{
			return await _unitOfWork.AnswerRepository.GetByIdAsync(id);
		}

		private async Task<bool> DoesQuestionExist(int questionId)
		{
			return (await _unitOfWork.QuestionRepository.GetByIdAsync(questionId)) is not null;
		}

		private async Task<bool> IsUnique(int questionId, string name)
		{
			var questions = (await _unitOfWork.QuestionRepository.GetAllAsync()).Where(q => q.Id == questionId);
			return !questions.Any(q => q.Answers.Any(a => a.Name == name));
		}

		private async Task<bool> CorrespondingToQuestionRestrictions(int questionId, bool isCorrectAnswer)
		{
			var question = await _unitOfWork.QuestionRepository.GetByIdAsync(questionId);
			var correctAnswersQuantity = question.Answers.Where(a => a.IsCorrect).Count();
			if (isCorrectAnswer && correctAnswersQuantity > 0 && question.Type == QuestionType.Single) return false;
			if (correctAnswersQuantity + 1 > DataRestrictions.AnswersMaxQuantity) return false;
			return true;
		}
	}
}
