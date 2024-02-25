using Data_Layer.FilterModels.QuizFilters;
using Data_Layer.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
    public interface IQuizService : IService<Quiz> 
    {
        Task<Guid> AddAsync(Quiz quiz, List<Guid> subcategoriesId);
        Task<List<Quiz?>> GetByPageFilter(GetQuizzesFilter filter);
        Task<bool> MatchSubcategories(Quiz quiz, List<Guid> subcategoriesId);
    }
}
