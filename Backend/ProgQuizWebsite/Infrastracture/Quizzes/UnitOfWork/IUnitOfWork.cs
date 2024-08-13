
using Core.Base.Repository;
using ProgQuizWebsite.Domain.Quizzes.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.Models.CategoryModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizContentModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.Quizzes.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<LanguageCategory> CategoryRepository { get; }
        IRepository<QuizSubcategory> SubcategoryRepository { get; }
        IRepository<Quiz> QuizRepository { get; }
        IRepository<Question> QuestionRepository { get; }
        IRepository<Answer> AnswerRepository { get; }
        IQuizRatingRepository QuizRatingRepository { get; }
        Task SaveAsync();
        void Save();
    }
}
