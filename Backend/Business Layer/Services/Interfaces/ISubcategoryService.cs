using Data_Layer.Models.CategoryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
    public interface ISubcategoryService
    {
        Task<List<QuizSubcategory?>> GetAll();
        Task<QuizSubcategory?> GetSubcategoryById(int id);
        Task<bool> AddSubcategory(QuizSubcategory quizSubcategory);
    }
}
