using Data_Layer.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<List<QuizSubcategory?>> GetAll();
        Task<QuizSubcategory?> GetSubcategoryById(int id);
        Task AddSubcategory(QuizSubcategory quizSubcategory);
    }
}
