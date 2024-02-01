using Data_Layer.Models.QuizModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Interfaces
{
    public interface IQuizRepository
    {
        Task<List<Quiz>> GetAll();
        Task<Quiz?> GetById(int id);
        Task AddQuiz(Quiz quiz);
    }
}
