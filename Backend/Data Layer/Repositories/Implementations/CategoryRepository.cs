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
    public class CategoryRepository : IRepository<LanguageCategory>
    {
        private readonly QuizAppContext _context;

        public CategoryRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task AddAsync(LanguageCategory category)
        {
            await _context.LanguageCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LanguageCategory?>> GetAllAsync()
        {
            return await _context.LanguageCategories.Include(lc => lc.Subcategories).ToListAsync();
        }

        public async Task<LanguageCategory?> GetByIdAsync(int id)
        {
            return await _context.LanguageCategories.Include(lc => lc.Subcategories)
                .Include(lc => lc.Quizzes)
                .FirstOrDefaultAsync(lc => lc.Id == id);
        }
    }
}
