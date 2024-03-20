
using Core.Base.Service.Interfaces;
using ProgQuizWebsite.Domain.FilterModels;
using ProgQuizWebsite.Domain.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgQuizWebsite.Services.Interfaces
{
    public interface IQuizService : IService<Quiz> 
    {
        Task<Guid> AddAsync(Quiz quiz, List<Guid> subcategoriesId);
        Task<List<Quiz?>> GetByPageFilter(GetQuizzesFilter filter);
        Task MatchSubcategories(Quiz quiz, List<Guid> subcategoriesId);
    }
}
