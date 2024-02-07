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


    public class QuizRepository : IRepository<Quiz>
    {
        private readonly QuizAppContext _context;

        public QuizRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Quiz quiz)
        {
            await _context.Quizzes.AddAsync(quiz);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await GetByIdAsync(id);
            _context.Quizzes.Remove(item);
        }

        public async Task<List<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes.Include(qz => qz.Subcategories)
                .ThenInclude(qz => qz.LanguageCategory).ToListAsync();
        }

        public async Task<Quiz?> GetByIdAsync(int id)
        {
            return await _context.Quizzes.Include(qz => qz.Questions).ThenInclude(q => q.Answers).Include(qz => qz.Subcategories)
                .ThenInclude(qz => qz.LanguageCategory)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
	}
}
