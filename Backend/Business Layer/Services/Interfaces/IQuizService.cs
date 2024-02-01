using Data_Layer.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.Services.Interfaces
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetAll();
        Task<Quiz?> GetById(int id);
        Task<bool> AddQuiz(Quiz quiz, List<int> subcategoriesId);
    }
}
