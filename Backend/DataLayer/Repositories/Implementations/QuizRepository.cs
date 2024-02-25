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


    public class QuizRepository : BaseRepository<Quiz>
    {
        private readonly QuizAppContext _context;

        public QuizRepository(QuizAppContext context) : base(context.Quizzes)
        {
            _context = context;
        }

        public override async Task<List<Quiz>> GetAllAsync()
        {
            return await _context.Quizzes.AsNoTracking().Include(qz => qz.LanguageCategory).Include(qz => qz.Subcategories)
                .ThenInclude(qz => qz.LanguageCategory).ToListAsync();
        }

        public override async Task<Quiz?> GetByGuidAsync(Guid id)
        {
            return await _context.Quizzes.Include(qz => qz.Questions).ThenInclude(q => q.Answers).Include(qz => qz.Subcategories)
                .ThenInclude(qz => qz.LanguageCategory)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
	}
}
