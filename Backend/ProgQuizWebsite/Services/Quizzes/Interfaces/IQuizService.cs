
using Core.Base.Service.Interfaces;
using ProgQuizWebsite.Domain.Quizzes.FilterModels;
using ProgQuizWebsite.Domain.Quizzes.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Quizzes.Interfaces
{
    public interface IQuizService : IService<Quiz>
    {
        Task<Guid> AddAsync(Quiz quiz, List<Guid> subcategoriesId);
        Task<List<Quiz?>> GetByFilterAsync(GetQuizzesFilter filter, HttpResponse response);
        Task MatchSubcategoriesAsync(Quiz quiz, List<Guid> subcategoriesId);
    }
}
