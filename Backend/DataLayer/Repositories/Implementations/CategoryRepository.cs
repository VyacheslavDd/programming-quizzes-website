using Data_Layer.Contexts;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Repositories.Implementations
{
    public class CategoryRepository : BaseRepository<LanguageCategory>
    {
        private readonly QuizAppContext _context;

        public CategoryRepository(QuizAppContext context) : base(context.LanguageCategories)
        {
            _context = context;
        }

		public override async Task<List<LanguageCategory?>> GetAllAsync()
        {
            return await _context.LanguageCategories.AsNoTracking().Include(lc => lc.Subcategories).ToListAsync();
        }

        public override async Task<LanguageCategory?> GetByIdAsync(int id)
        {
            return await _context.LanguageCategories.Include(lc => lc.Subcategories).ThenInclude(lc => lc.Quizzes)
                .FirstOrDefaultAsync(lc => lc.Id == id);
        }
	}
}
