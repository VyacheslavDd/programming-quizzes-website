using Data_Layer.Models.CategoryModels;
using Data_Layer.Models.QuizContentModels;
using Data_Layer.Models.QuizModels;
using Data_Layer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.UnitOfWork
{
    public interface IUnitOfWork
    {
        IRepository<LanguageCategory> CategoryRepository { get; }
        IRepository<QuizSubcategory> SubcategoryRepository { get; }
        IRepository<Quiz> QuizRepository { get; }
        IRepository<Question> QuestionRepository { get; }
        IRepository<Answer> AnswerRepository { get; }
        Task Save();
    }
}
