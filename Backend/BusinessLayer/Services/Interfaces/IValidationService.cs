using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
	public interface IValidationService
	{
		public Task ValidateAnswer(Answer? answer, IRepository<Question> questionRepository);
		public Task ValidateCategory(LanguageCategory? category, IRepository<LanguageCategory> categoryRepository);
		public Task ValidateQuestion(Question? question, IRepository<Quiz> quizRepository);
		public Task ValidateQuiz(Quiz? quiz, IRepository<LanguageCategory> categoryRepository, IRepository<Quiz> quizRepository);
		public Task ValidateSubcategory(QuizSubcategory? subcategory, IRepository<LanguageCategory> categoryRepository,
			IRepository<QuizSubcategory> subcategoryRepository);
	}
}
