
using Core.Base.Repository;
using ProgQuizWebsite.Domain.CategoryModels;
using ProgQuizWebsite.Domain.QuizContentModels;
using ProgQuizWebsite.Domain.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Infrastracture.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<LanguageCategory> CategoryRepository { get; }
        IRepository<QuizSubcategory> SubcategoryRepository { get; }
        IRepository<Quiz> QuizRepository { get; }
        IRepository<Question> QuestionRepository { get; }
        IRepository<Answer> AnswerRepository { get; }
        Task SaveAsync();
        void Save();
    }
}
