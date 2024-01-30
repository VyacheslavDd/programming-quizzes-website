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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QuizAppContext _context;

        public CategoryRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task AddCategory(LanguageCategory category)
        {
            await _context.LanguageCategories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LanguageCategory?>> GetAll()
        {
            return await _context.LanguageCategories.ToListAsync();
        }

        public async Task<LanguageCategory?> GetById(int id)
        {
            return await _context.LanguageCategories.FindAsync(id);
        }

        public async Task<List<QuizSubcategory?>> GetSubcategories(int id)
        {
            return await _context.Subcategories
                .Where(sc => sc.LanguageCategoryId == id)
                .Include(qz => qz.LanguageCategory)
                .ToListAsync();
        }
    }
}
