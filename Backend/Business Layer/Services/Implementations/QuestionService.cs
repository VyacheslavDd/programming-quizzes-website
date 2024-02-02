using Business_Layer.Services.Interfaces;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Implementations
{
	public class QuestionService : IService<Question>
	{
		private readonly IUnitOfWork _unitOfWork;

		public QuestionService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> AddAsync(Question? question)
		{
			var doesQuizExist = await DoesQuizExist(question.QuizId);
			if (!doesQuizExist) return false;
			var isUnique = await IsUnique(question.QuizId, question.Title);
			if (!isUnique) return false;
			try
			{
				await _unitOfWork.QuestionRepository.AddAsync(question);
				await _unitOfWork.Save();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<Question?>> GetAllAsync()
		{
			return await _unitOfWork.QuestionRepository.GetAllAsync();
		}

		public async Task<Question?> GetByIdAsync(int id)
		{
			return await _unitOfWork.QuestionRepository.GetByIdAsync(id);
		}

		private async Task<bool> DoesQuizExist(int quizId)
		{
			return (await _unitOfWork.QuizRepository.GetByIdAsync(quizId)) is not null;
		}

		private async Task<bool> IsUnique(int quizId, string? title)
		{
			var entries = (await _unitOfWork.QuizRepository.GetAllAsync()).Where(qz => qz.Id == quizId);
			return !entries.Any(qz => qz.Questions.Any(q => q.Title == title));
		}
	}
}
