using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Layer.Contexts;
using Data_Layer.Models.CategoryModels;
using Data_Layer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Repositories.Implementations
{
    public class SubcategoryRepository : IRepository<QuizSubcategory>
    {
        private readonly QuizAppContext _context;

        public SubcategoryRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task AddAsync(QuizSubcategory quizSubcategory)
        {
            await _context.Subcategories.AddAsync(quizSubcategory);
        }

		public async Task DeleteAsync(int id)
		{
            var item = await GetByIdAsync(id);
            _context.Subcategories.Remove(item);
		}

		public async Task<List<QuizSubcategory?>> GetAllAsync()
        {
            return await _context.Subcategories.Include(qz => qz.LanguageCategory).ToListAsync();
        }

        public async Task<QuizSubcategory?> GetByIdAsync(int id)
        {
            return await _context.Subcategories.Include(qz => qz.LanguageCategory)
                .FirstOrDefaultAsync(qz => qz.Id == id);
        }
    }
}
