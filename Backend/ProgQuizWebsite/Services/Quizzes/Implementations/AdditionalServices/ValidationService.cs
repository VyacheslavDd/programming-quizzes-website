
using Core.Base.Repository;
using Core.Constants;
using Core.Enums;
using Core.Exceptions.QuizElementsExceptions;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using ProgQuizWebsite.Services.Quizzes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Quizzes.Implementations.AdditionalServices
{
    public class ValidationService : IValidationService
    {
        public async Task ValidateAnswer(Answer? answer, IRepository<Question> questionRepository)
        {
            var question = await questionRepository.GetByGuidAsync(answer.QuestionId);
            if (question is null) throw new ArgumentNullException("Указанный вопрос не существует!");
            if (question.Answers is not null && question.Answers.Any(a => a.Name.ToLower() == answer.Name.ToLower()))
                throw new NotUniqueException("Указанный ответ уже существует в викторине");
            var correctAnswersQuantity = question.Answers.Where(a => a.IsCorrect).Count();
            if (answer.IsCorrect && correctAnswersQuantity > 0 && question.Type == QuestionType.Single)
                throw new CorrectAnswersAndQuestionTypeUnmatchingException("Вопрос с единственным вариантом ответа не может иметь несколько правильных ответов!");
            if (correctAnswersQuantity + 1 > DataRestrictions.AnswersMaxQuantity)
                throw new AnswersOverflowException("Превышено допустимое количество вариантов ответа на вопрос!");
        }

        public async Task ValidateCategory(LanguageCategory? category, IRepository<LanguageCategory> categoryRepository)
        {
            var categories = await categoryRepository.GetAllAsync();
            if (categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
                throw new NotUniqueException("Введённая категория уже существует!");
        }

        public async Task ValidateQuestion(Question? question, IRepository<Quiz> quizRepository)
        {
            var quiz = await quizRepository.GetByGuidAsync(question.QuizId);
            if (quiz is null) throw new ArgumentNullException("Указанная викторина не существует!");
            if (quiz.Questions is not null && quiz.Questions.Any(q => q.Title.ToLower() == question.Title.ToLower()))
                throw new NotUniqueException("Указанный вопрос уже существует в викторине");
            if (question.Answers is not null && question.Type == QuestionType.Single && question.Answers.Where(a => a.IsCorrect).Count() > 1)
                throw new CorrectAnswersAndQuestionTypeUnmatchingException("Невозможно сменить тип ответа на одиночный выбор, поскольку существует несколько вариантов ответа!");
        }

        public async Task ValidateQuiz(Quiz? quiz, IRepository<LanguageCategory> categoryRepository, IRepository<Quiz> quizRepository)
        {
            if (await categoryRepository.GetByGuidAsync(quiz.LanguageCategoryId) is null) throw new ArgumentNullException("Указанная категория не существует!");
            var quizzes = (await quizRepository.GetAllAsync()).Where(qz => qz.LanguageCategoryId == quiz.LanguageCategoryId);
            if (quizzes.Any(qz => qz.Title.ToLower() == quiz.Title.ToLower()))
                throw new NotUniqueException("Указанная викторина уже существует!");
        }

        public async Task ValidateSubcategory(QuizSubcategory? subcategory, IRepository<LanguageCategory> categoryRepository,
            IRepository<QuizSubcategory> subcategoryRepository)
        {
            var categories = await categoryRepository.GetAllAsync();
            if (!categories.Any(c => c.Id == subcategory.LanguageCategoryId)) throw new ArgumentNullException("Указанная категория не существует!");
            var entries = (await subcategoryRepository.GetAllAsync()).Where(sc => sc.LanguageCategoryId == subcategory.LanguageCategoryId);
            if (entries.Any(sc => sc.Name.ToLower() == subcategory.Name.ToLower())) throw new NotUniqueException("Указанная подкатегория уже существует!");
        }
    }
}
