
using Core.Base.Repository;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Quizzes.Interfaces
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
