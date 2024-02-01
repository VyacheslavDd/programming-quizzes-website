using Data_Layer.Contexts;
using Data_Layer.Models.QuizModels;
using Data_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Implementations
{


    public class QuizRepository : IQuizRepository
    {
        private readonly QuizAppContext _context;

        public QuizRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task AddQuiz(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Quiz>> GetAll()
        {
            return await _context.Quizzes.Include(qz => qz.LanguageCategory)
                .Include(qz => qz.Subcategories).ToListAsync();
        }

        public async Task<Quiz?> GetById(int id)
        {
            return await _context.Quizzes.Include(qz => qz.LanguageCategory)
                .Include(qz => qz.Subcategories).Include(qz => qz.Questions)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
    }
}
